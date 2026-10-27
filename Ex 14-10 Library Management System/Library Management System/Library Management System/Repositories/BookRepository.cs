using Library_Management_System.Data;
using Library_Management_System.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            // FIXED: Added .Include so the database joins the Authors table and returns the author data
            return await _context.Books.Include(b => b.Author).ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            // FIXED: Changed FindAsync to FirstOrDefaultAsync to allow .Include
            return await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            var local = _context.Books.Local.FirstOrDefault(b => b.Id == book.Id);
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

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

        public async Task<IEnumerable<Book>> SearchBooksAsync(string? title, string? author, string? isbn, string? category)
        {
            // FIXED: Added .Include to the query
            var query = _context.Books.Include(b => b.Author).AsQueryable();

            if (!string.IsNullOrEmpty(title))
                query = query.Where(b => b.Title.Contains(title));

            if (!string.IsNullOrEmpty(author))
                // FIXED: Now correctly searches inside the Author's Name property
                query = query.Where(b => b.Author != null && b.Author.Name.Contains(author));

            if (!string.IsNullOrEmpty(isbn))
                query = query.Where(b => b.ISBN == isbn);

            if (!string.IsNullOrEmpty(category))
                query = query.Where(b => b.Category == category);

            return await query.ToListAsync();
        }
    }
}