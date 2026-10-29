using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Repositories
{
    public interface IIssueRepository
    {
        Task<IEnumerable<IssueMaster>> GetAllAsync();
        Task<IssueMaster?> GetByIdAsync(string id);
        Task<IssueMaster> AddAsync(IssueMaster issue);
        Task UpdateAsync(IssueMaster issue);
    }

    public class IssueRepository : IIssueRepository
    {
        private readonly NewBooksDbContext _context;

        public IssueRepository(NewBooksDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IssueMaster>> GetAllAsync()
        {
            return await _context.IssueMasters.ToListAsync();
        }

        public async Task<IssueMaster?> GetByIdAsync(string id)
        {
            return await _context.IssueMasters.FindAsync(id);
        }

        public async Task<IssueMaster> AddAsync(IssueMaster issue)
        {
            await _context.IssueMasters.AddAsync(issue);
            await _context.SaveChangesAsync();
            return issue;
        }

        public async Task UpdateAsync(IssueMaster issue)
        {
            _context.IssueMasters.Update(issue);
            await _context.SaveChangesAsync();
        }
    }
}