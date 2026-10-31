using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Repositories
{
    public interface IPaymentMasterRepository
    {
        Task<IEnumerable<PaymentMaster>> GetAllAsync();
        Task<PaymentMaster?> GetByIdAsync(string id);
        Task<PaymentMaster> AddAsync(PaymentMaster payment);
        Task UpdateAsync(PaymentMaster payment);
    }

    public class PaymentMasterRepository : IPaymentMasterRepository
    {
        private readonly NewBooksDbContext _context;

        public PaymentMasterRepository(NewBooksDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentMaster>> GetAllAsync()
        {
            return await _context.PaymentMasters.ToListAsync();
        }

        public async Task<PaymentMaster?> GetByIdAsync(string id)
        {
            return await _context.PaymentMasters.FindAsync(id);
        }

        public async Task<PaymentMaster> AddAsync(PaymentMaster payment)
        {
            await _context.PaymentMasters.AddAsync(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task UpdateAsync(PaymentMaster payment)
        {
            _context.PaymentMasters.Update(payment);
            await _context.SaveChangesAsync();
        }
    }
}