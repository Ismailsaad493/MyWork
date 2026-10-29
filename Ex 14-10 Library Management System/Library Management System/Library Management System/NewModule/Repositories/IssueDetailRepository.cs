using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Repositories
{
    public interface IIssueDetailRepository
    {
        Task<IEnumerable<IssueDetailMaster>> GetAllAsync();
        Task<IssueDetailMaster?> GetByIdAsync(int id);
        Task<IssueDetailMaster> AddAsync(IssueDetailMaster detail);
        Task UpdateAsync(IssueDetailMaster detail);
    }

    public class IssueDetailRepository : IIssueDetailRepository
    {
        private readonly NewBooksDbContext _context;

        public IssueDetailRepository(NewBooksDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IssueDetailMaster>> GetAllAsync()
        {
            return await _context.IssueDetails.ToListAsync();
        }

        public async Task<IssueDetailMaster?> GetByIdAsync(int id)
        {
            return await _context.IssueDetails.FindAsync(id);
        }

        public async Task<IssueDetailMaster> AddAsync(IssueDetailMaster detail)
        {
            await _context.IssueDetails.AddAsync(detail);
            await _context.SaveChangesAsync();
            return detail;
        }

        public async Task UpdateAsync(IssueDetailMaster detail)
        {
            _context.IssueDetails.Update(detail);
            await _context.SaveChangesAsync();
        }
    }
}