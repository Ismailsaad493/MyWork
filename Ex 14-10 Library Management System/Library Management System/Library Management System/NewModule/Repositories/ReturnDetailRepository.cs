using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Repositories
{
    public interface IReturnDetailRepository
    {
        Task<IEnumerable<ReturnDetailMaster>> GetAllAsync();
        Task<ReturnDetailMaster?> GetByIdAsync(string id);
        Task<ReturnDetailMaster> AddAsync(ReturnDetailMaster returnDetail);
        Task UpdateAsync(ReturnDetailMaster returnDetail);
    }

    public class ReturnDetailRepository : IReturnDetailRepository
    {
        private readonly NewBooksDbContext _context;

        public ReturnDetailRepository(NewBooksDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReturnDetailMaster>> GetAllAsync()
        {
            return await _context.ReturnDetails.ToListAsync();
        }

        public async Task<ReturnDetailMaster?> GetByIdAsync(string id)
        {
            return await _context.ReturnDetails.FindAsync(id);
        }

        public async Task<ReturnDetailMaster> AddAsync(ReturnDetailMaster returnDetail)
        {
            await _context.ReturnDetails.AddAsync(returnDetail);
            await _context.SaveChangesAsync();
            return returnDetail;
        }

        public async Task UpdateAsync(ReturnDetailMaster returnDetail)
        {
            _context.ReturnDetails.Update(returnDetail);
            await _context.SaveChangesAsync();
        }
    }
}