using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    public interface IPaymentMasterService
    {
        Task<IEnumerable<PaymentMaster>> GetAllPaymentsAsync();
        Task<PaymentMaster> CreatePaymentAsync(PaymentCreateDto dto);
        Task<bool> UpdatePaymentAsync(string id, PaymentUpdateDto dto);
    }

    public class PaymentMasterService : IPaymentMasterService
    {
        private readonly IPaymentMasterRepository _repo;

        public PaymentMasterService(IPaymentMasterRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PaymentMaster>> GetAllPaymentsAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<PaymentMaster> CreatePaymentAsync(PaymentCreateDto dto)
        {
            var payment = new PaymentMaster
            {
                SId = dto.SId,
                PaymentMethod = dto.PaymentMethod,
                TotalAmount = 0, // Auto-calculates when Detail is created
                PaymentDate = DateTime.UtcNow
            };
            return await _repo.AddAsync(payment);
        }

        public async Task<bool> UpdatePaymentAsync(string id, PaymentUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.SId = dto.SId;
            existing.PaymentMethod = dto.PaymentMethod;

            await _repo.UpdateAsync(existing);
            return true;
        }
    }
}