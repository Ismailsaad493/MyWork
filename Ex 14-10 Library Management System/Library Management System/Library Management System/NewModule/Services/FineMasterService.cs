using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    public interface IFineMasterService
    {
        Task<IEnumerable<FineMaster>> GetAllFinesAsync();
        Task<FineMaster> CreateFineAsync(FineCreateDto dto);
        Task<bool> UpdateFineAsync(string id, FineUpdateDto dto);
    }

    public class FineMasterService : IFineMasterService
    {
        private readonly IFineMasterRepository _repo;
        private readonly IFineDetailMasterRepository _fineDetailRepo;
        private readonly IPaymentDetailMasterRepository _paymentDetailRepo;
        private readonly IPaymentMasterRepository _paymentRepo;

        public FineMasterService(
            IFineMasterRepository repo,
            IFineDetailMasterRepository fineDetailRepo,
            IPaymentDetailMasterRepository paymentDetailRepo,
            IPaymentMasterRepository paymentRepo)
        {
            _repo = repo;
            _fineDetailRepo = fineDetailRepo;
            _paymentDetailRepo = paymentDetailRepo;
            _paymentRepo = paymentRepo;
        }

        public async Task<IEnumerable<FineMaster>> GetAllFinesAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<FineMaster> CreateFineAsync(FineCreateDto dto)
        {
            var fine = new FineMaster
            {
                IssueDetailId = dto.IssueDetailId,
                FineAmount = dto.FineAmount,
                Status = dto.Status,
                FineDate = DateTime.UtcNow
            };
            return await _repo.AddAsync(fine);
        }

        public async Task<bool> UpdateFineAsync(string id, FineUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.IssueDetailId = dto.IssueDetailId;
            existing.Status = dto.Status;

            bool amountChanged = existing.FineAmount != dto.FineAmount;
            existing.FineAmount = dto.FineAmount;

            await _repo.UpdateAsync(existing);

            if (amountChanged)
            {
                // Cascade update to Fine Detail Master
                var fineDetails = await _fineDetailRepo.GetByFineIdAsync(id);
                foreach (var fd in fineDetails)
                {
                    fd.Amount = dto.FineAmount;
                    await _fineDetailRepo.UpdateAsync(fd);
                }

                // Cascade update to Payment Detail Master and Payment Master
                var paymentDetails = await _paymentDetailRepo.GetByFineIdAsync(id);
                foreach (var pd in paymentDetails)
                {
                    pd.PaidAmount = dto.FineAmount;
                    await _paymentDetailRepo.UpdateAsync(pd);

                    var payment = await _paymentRepo.GetByIdAsync(pd.PaymentId);
                    if (payment != null)
                    {
                        payment.TotalAmount = dto.FineAmount;
                        await _paymentRepo.UpdateAsync(payment);
                    }
                }
            }

            return true;
        }
    }
}