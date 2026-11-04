using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Repositories
{
    // ==========================================
    // 1. THE INTERFACE (Contract)
    // ==========================================
    public interface IBookCopyRepository
    {
        Task<IEnumerable<BookCopy>> GetAllAsync(int pageNumber, int pageSize); Task<BookCopy> AddAsync(BookCopy copy);
        Task<BookCopy?> GetByIdAsync(int id);
        Task UpdateAsync(BookCopy copy);
        Task DeleteAsync(BookCopy copy);
    }

    // ==========================================
    // 2. THE IMPLEMENTATION
    // ==========================================
    public class BookCopyRepository : IBookCopyRepository
    {
        private readonly NewBooksDbContext _context;
        public BookCopyRepository(NewBooksDbContext context) => _context = context;

        public async Task<IEnumerable<BookCopy>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.BookCopies
                                 .IgnoreQueryFilters()
                                 .Include(bc => bc.Book)
                                 .Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        }

        public async Task<BookCopy> AddAsync(BookCopy copy)
        {
            await _context.BookCopies.AddAsync(copy);
            await _context.SaveChangesAsync();
            return copy;
        }

        public async Task<BookCopy?> GetByIdAsync(int id)
        {
            return await _context.BookCopies
                                 .IgnoreQueryFilters()
                                 .Include(bc => bc.Book) // Added this join to fetch the Book Master table
                                 .FirstOrDefaultAsync(bc => bc.BookCopyId == id);
        }

        public async Task UpdateAsync(BookCopy copy)
        {
            _context.BookCopies.Update(copy);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(BookCopy copy)
        {
            await _context.BookCopies
                .Where(bc => bc.BookCopyId == copy.BookCopyId)
                .ExecuteDeleteAsync();
        }
    }
}