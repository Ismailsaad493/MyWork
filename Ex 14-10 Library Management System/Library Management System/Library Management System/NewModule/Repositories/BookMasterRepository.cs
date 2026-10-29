using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Repositories
{
    public class BookMasterRepository : IBookMasterRepository
    {
        private readonly NewBooksDbContext _context;

        public BookMasterRepository(NewBooksDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookMaster>> GetAllAsync()
        {
            return await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .ToListAsync();
        }

        public async Task<BookMaster?> GetByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(b => b.BookId == id);
        }

        public async Task<BookMaster> AddAsync(BookMaster book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<bool> IsbnExistsAsync(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                return false;

            var normalizedIsbn = isbn.Trim().ToLower();

            return await _context.Books
                .AnyAsync(b => b.Isbn != null && b.Isbn.ToLower() == normalizedIsbn);
        }

        // NEW: For Update
        public async Task UpdateAsync(BookMaster book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
    }
}