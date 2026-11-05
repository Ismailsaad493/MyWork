using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;
using System;
using System.Collections.Generic;
using System.Linq; // Required for mapping
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    public interface IFineMasterService
    {
        // Changed to return DTOs instead of raw Entities
        Task<IEnumerable<FineResponseDto>> GetAllFinesAsync();
        Task<FineResponseDto> CreateFineAsync(FineCreateDto dto);
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

        public async Task<IEnumerable<FineResponseDto>> GetAllFinesAsync()
        {
            var fines = await _repo.GetAllAsync();

            // Map the raw database entities to safe Response DTOs
            return fines.Select(f => new FineResponseDto
            {
                FineId = f.FineId.ToString(), // Safely handles the ID
                IssueDetailId = f.IssueDetailId,
                FineDate = f.FineDate,
                FineAmount = f.FineAmount,
                Status = f.Status
            }).ToList();
        }

        public async Task<FineResponseDto> CreateFineAsync(FineCreateDto dto)
        {
            var fine = new FineMaster
            {
                IssueDetailId = dto.IssueDetailId,
                FineAmount = dto.FineAmount,
                Status = dto.Status,
                FineDate = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            var savedFine = await _repo.AddAsync(fine);

            // Return the mapped DTO
            return new FineResponseDto
            {
                FineId = savedFine.FineId.ToString(),
                IssueDetailId = savedFine.IssueDetailId,
                FineDate = savedFine.FineDate,
                FineAmount = savedFine.FineAmount,
                Status = savedFine.Status
            };
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