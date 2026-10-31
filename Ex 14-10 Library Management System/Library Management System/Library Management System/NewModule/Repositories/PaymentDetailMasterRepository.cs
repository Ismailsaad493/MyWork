using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Repositories
{
    public interface IPaymentDetailMasterRepository
    {
        Task<IEnumerable<PaymentDetailMaster>> GetAllAsync();
        Task<PaymentDetailMaster?> GetByIdAsync(string id);
        Task<PaymentDetailMaster> AddAsync(PaymentDetailMaster paymentDetail);
        Task UpdateAsync(PaymentDetailMaster paymentDetail);
    }

    public class PaymentDetailMasterRepository : IPaymentDetailMasterRepository
    {
        private readonly NewBooksDbContext _context;

        public PaymentDetailMasterRepository(NewBooksDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentDetailMaster>> GetAllAsync()
        {
            return await _context.PaymentDetails.ToListAsync();
        }

        public async Task<PaymentDetailMaster?> GetByIdAsync(string id)
        {
            return await _context.PaymentDetails.FindAsync(id);
        }

        public async Task<PaymentDetailMaster> AddAsync(PaymentDetailMaster paymentDetail)
        {
            await _context.PaymentDetails.AddAsync(paymentDetail);
            await _context.SaveChangesAsync();
            return paymentDetail;
        }

        public async Task UpdateAsync(PaymentDetailMaster paymentDetail)
        {
            _context.PaymentDetails.Update(paymentDetail);
            await _context.SaveChangesAsync();
        }
    }
}