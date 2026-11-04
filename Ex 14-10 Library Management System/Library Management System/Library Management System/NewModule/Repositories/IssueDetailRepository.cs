using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Repositories
{
    public interface IIssueDetailRepository
    {
        Task<IEnumerable<IssueDetailMaster>> GetAllAsync(int pageNumber, int pageSize);
        Task<IssueDetailMaster> AddAsync(IssueDetailMaster detail);
        Task<IssueDetailMaster?> GetByIdAsync(int id);
        Task UpdateAsync(IssueDetailMaster detail);
        Task DeleteAsync(IssueDetailMaster detail);
    }

    public class IssueDetailRepository : IIssueDetailRepository
    {
        private readonly NewBooksDbContext _context;
        public IssueDetailRepository(NewBooksDbContext context) => _context = context;

        public async Task<IEnumerable<IssueDetailMaster>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.IssueDetails
                                 .IgnoreQueryFilters()
                                 // Jumps from IssueDetail -> BookCopy -> Book to fetch the title
                                 .Include(d => d.BookCopy)
                                    .ThenInclude(bc => bc.Book)
                                 .Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        }

        public async Task<IssueDetailMaster> AddAsync(IssueDetailMaster detail)
        {
            await _context.IssueDetails.AddAsync(detail);
            await _context.SaveChangesAsync();
            return detail;
        }

        public async Task<IssueDetailMaster?> GetByIdAsync(int id)
        {
            return await _context.IssueDetails
                                 .IgnoreQueryFilters()
                                 .FirstOrDefaultAsync(d => d.IssueDetailId == id);
        }

        public async Task UpdateAsync(IssueDetailMaster detail)
        {
            _context.IssueDetails.Update(detail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(IssueDetailMaster detail)
        {
            await _context.IssueDetails
                .Where(d => d.IssueDetailId == detail.IssueDetailId)
                .ExecuteDeleteAsync();
        }
    }
}