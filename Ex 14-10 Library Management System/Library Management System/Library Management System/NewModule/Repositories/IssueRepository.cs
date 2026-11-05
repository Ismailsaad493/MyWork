using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq; // Required for filtering and numbering
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Repositories
{
    public interface IIssueRepository
    {
        Task<IEnumerable<IssueMaster>> GetAllAsync();
        Task<IssueMaster?> GetByIdAsync(string id);
        Task<IssueMaster> AddAsync(IssueMaster issue);
        Task UpdateAsync(IssueMaster issue);
        Task DeleteAsync(IssueMaster issue);
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
            // Include details so the child list is fetched from SQL
            return await _context.IssueMasters
                .Include(i => i.Details)
                .ToListAsync();
        }

        public async Task<IssueMaster?> GetByIdAsync(string id)
        {
            // Include details here as well
            return await _context.IssueMasters
                .Include(i => i.Details)
                .FirstOrDefaultAsync(i => i.IssueId == id);
        }

        public async Task<IssueMaster> AddAsync(IssueMaster issue)
        {
            // 1. Fetch all existing IssueIds for this specific Student (SId)
            var existingIds = await _context.IssueMasters
                .Where(i => i.SId == issue.SId)
                .Select(i => i.IssueId)
                .ToListAsync();

            // 2. Calculate the next sequence number (i1, i2, etc.)
            int nextNumber = 1;
            if (existingIds.Any())
            {
                nextNumber = existingIds
                    .Select(id =>
                    {
                        if (id.StartsWith("i") && int.TryParse(id.Substring(1), out int num))
                            return num;
                        return 0;
                    })
                    .Max() + 1;
            }

            // 3. Assign the generated IssueId automatically
            issue.IssueId = $"i{nextNumber}";

            await _context.IssueMasters.AddAsync(issue);
            await _context.SaveChangesAsync();
            return issue;
        }

        public async Task UpdateAsync(IssueMaster issue)
        {
            _context.IssueMasters.Update(issue);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(IssueMaster issue)
        {
            _context.IssueMasters.Remove(issue);
            await _context.SaveChangesAsync();
        }
    }
}