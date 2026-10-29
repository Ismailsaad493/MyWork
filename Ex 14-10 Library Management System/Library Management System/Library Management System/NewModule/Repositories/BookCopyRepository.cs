using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Repositories
{
    // ==========================================
    // 1. THE INTERFACE (Contract)
    // ==========================================
    public interface IBookCopyRepository
    {
        Task<IEnumerable<BookCopy>> GetAllAsync();
        Task<BookCopy> AddAsync(BookCopy copy);

        // FIXED: Added these two methods to resolve the errors in your Service
        Task<BookCopy?> GetByIdAsync(int id);
        Task UpdateAsync(BookCopy copy);
    }

    // ==========================================
    // 2. THE IMPLEMENTATION
    // ==========================================
    public class BookCopyRepository : IBookCopyRepository
    {
        private readonly NewBooksDbContext _context;

        public BookCopyRepository(NewBooksDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookCopy>> GetAllAsync()
        {
            return await _context.BookCopies.ToListAsync();
        }

        public async Task<BookCopy> AddAsync(BookCopy copy)
        {
            await _context.BookCopies.AddAsync(copy);
            await _context.SaveChangesAsync();
            return copy;
        }

        // FIXED: Implemented the fetch logic
        public async Task<BookCopy?> GetByIdAsync(int id)
        {
            return await _context.BookCopies.FindAsync(id);
        }

        // FIXED: Implemented the update logic
        public async Task UpdateAsync(BookCopy copy)
        {
            _context.BookCopies.Update(copy);
            await _context.SaveChangesAsync();
        }
    }
}