using Library_Management_System.Data;
using Library_Management_System.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Repositories
{
    public interface IFineRepository
    {
        Task<Fine> AddAsync(Fine fine);
        Task<Fine?> GetByIdAsync(int id);
        Task<IEnumerable<Fine>> GetByStudentIdAsync(int studentId);
        Task UpdateAsync(Fine fine);
    }

    public class FineRepository : IFineRepository
    {
        private readonly AppDbContext _context;

        public FineRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Fine> AddAsync(Fine fine)
        {
            await _context.Fines.AddAsync(fine);
            await _context.SaveChangesAsync();
            return fine;
        }

        public async Task<Fine?> GetByIdAsync(int id)
        {
            return await _context.Fines.FindAsync(id);
        }

        public async Task<IEnumerable<Fine>> GetByStudentIdAsync(int studentId)
        {
            return await _context.Fines
                .Where(f => f.StudentId == studentId)
                .ToListAsync();
        }

        public async Task UpdateAsync(Fine fine)
        {
            _context.Fines.Update(fine);
            await _context.SaveChangesAsync();
        }
    }
}