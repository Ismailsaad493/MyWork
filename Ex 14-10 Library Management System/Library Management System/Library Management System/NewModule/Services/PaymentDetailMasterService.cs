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
        Task<PaymentDetailMaster> CreateDetailAsync(PaymentDetailCreateDto dto);
        Task<bool> UpdateDetailAsync(string id, PaymentDetailUpdateDto dto);
    }

    public class PaymentDetailMasterService : IPaymentDetailMasterService
    {
        private readonly IPaymentDetailMasterRepository _repo;

        public PaymentDetailMasterService(IPaymentDetailMasterRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PaymentDetailMaster>> GetAllDetailsAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<PaymentDetailMaster> CreateDetailAsync(PaymentDetailCreateDto dto)
        {
            var detail = new PaymentDetailMaster
            {
                PaymentId = dto.PaymentId,
                FineId = dto.FineId,
                PaidAmount = dto.PaidAmount
            };
            return await _repo.AddAsync(detail);
        }

        public async Task<bool> UpdateDetailAsync(string id, PaymentDetailUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.PaymentId = dto.PaymentId;
            existing.FineId = dto.FineId;
            existing.PaidAmount = dto.PaidAmount;

            await _repo.UpdateAsync(existing);
            return true;
        }
    }
}