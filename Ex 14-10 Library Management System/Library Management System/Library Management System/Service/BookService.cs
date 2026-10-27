using Library_Management_System.Data;
using Library_Management_System.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Service
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string? title, string? author)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(b => b.Title.Contains(title));
            if (!string.IsNullOrWhiteSpace(author))
                query = query.Where(b => b.Author.Contains(author));

            return await query.ToListAsync();
        }

        public async Task AddBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}