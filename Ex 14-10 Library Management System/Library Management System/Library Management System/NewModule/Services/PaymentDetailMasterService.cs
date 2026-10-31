using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    public interface IPaymentDetailMasterService
    {
        Task<IEnumerable<PaymentDetailMaster>> GetAllDetailsAsync();
        Task<PaymentDetailMaster?> CreateDetailAsync(PaymentDetailCreateDto dto);
        Task<bool> UpdateDetailAsync(string id, PaymentDetailUpdateDto dto);
    }

    public class PaymentDetailMasterService : IPaymentDetailMasterService
    {
        private readonly IPaymentDetailMasterRepository _repo;
        private readonly IFineMasterRepository _fineRepo;
        private readonly IPaymentMasterRepository _paymentRepo;

        public PaymentDetailMasterService(
            IPaymentDetailMasterRepository repo,
            IFineMasterRepository fineRepo,
            IPaymentMasterRepository paymentRepo)
        {
            _repo = repo;
            _fineRepo = fineRepo;
            _paymentRepo = paymentRepo;
        }

        public async Task<IEnumerable<PaymentDetailMaster>> GetAllDetailsAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<PaymentDetailMaster?> CreateDetailAsync(PaymentDetailCreateDto dto)
        {
            var fine = await _fineRepo.GetByIdAsync(dto.FineId);
            if (fine == null) return null;

            var detail = new PaymentDetailMaster
            {
                PaymentId = dto.PaymentId,
                FineId = dto.FineId,
                PaidAmount = fine.FineAmount // Auto-populated
            };

            var savedDetail = await _repo.AddAsync(detail);

            var payment = await _paymentRepo.GetByIdAsync(dto.PaymentId);
            if (payment != null)
            {
                payment.TotalAmount = fine.FineAmount;
                await _paymentRepo.UpdateAsync(payment);
            }

            return savedDetail;
        }

        public async Task<bool> UpdateDetailAsync(string id, PaymentDetailUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            var fine = await _fineRepo.GetByIdAsync(dto.FineId);
            if (fine == null) return false;

            existing.PaymentId = dto.PaymentId;
            existing.FineId = dto.FineId;
            existing.PaidAmount = fine.FineAmount; // Auto-synced

            await _repo.UpdateAsync(existing);

            var payment = await _paymentRepo.GetByIdAsync(existing.PaymentId);
            if (payment != null)
            {
                payment.TotalAmount = fine.FineAmount;
                await _paymentRepo.UpdateAsync(payment);
            }

            return true;
        }
    }
}