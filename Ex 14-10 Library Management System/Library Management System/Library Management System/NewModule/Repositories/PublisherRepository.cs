using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq; // Required for LINQ commands like Skip, Take, and Where
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Repositories
{
    public interface IPublisherRepository
    {
        // FIX: Added pagination parameters here
        Task<IEnumerable<PublisherMaster>> GetAllAsync(int pageNumber, int pageSize);
        Task<PublisherMaster> AddAsync(PublisherMaster publisher);

        // NEW: For Update
        Task<PublisherMaster?> GetByIdAsync(int id);
        Task UpdateAsync(PublisherMaster publisher);
        Task DeleteAsync(PublisherMaster publisher);
        Task<bool> HardDeleteAsync(int id);
    }

    public class PublisherRepository : IPublisherRepository
    {
        private readonly NewBooksDbContext _context;
        public PublisherRepository(NewBooksDbContext context) => _context = context;

        // FIX: Added Skip and Take logic for pagination
        public async Task<IEnumerable<PublisherMaster>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.Publishers
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<PublisherMaster> AddAsync(PublisherMaster publisher)
        {
            await _context.Publishers.AddAsync(publisher);
            await _context.SaveChangesAsync();
            return publisher;
        }

        // NEW: For Update
        public async Task<PublisherMaster?> GetByIdAsync(int id)
        {
            return await _context.Publishers.FindAsync(id);
        }

        public async Task UpdateAsync(PublisherMaster publisher)
        {
            _context.Publishers.Update(publisher);
            await _context.SaveChangesAsync();
        }

        // --- SOFT DELETE ---
        public async Task DeleteAsync(PublisherMaster publisher)
        {
            // DbContext intercepts this and sets IsActive = false
            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
        }

        // --- HARD DELETE ---
        public async Task<bool> HardDeleteAsync(int id)
        {
            // Bypasses interceptor, deletes from SQL directly
            int deletedRows = await _context.Publishers
                .Where(p => p.PId == id)
                .ExecuteDeleteAsync();

            return deletedRows > 0;
        }
    }
}