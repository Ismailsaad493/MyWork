using Book_Library_API.Data;
using Book_Library_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Book_Library_API.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _dbContext;

        public BookRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Book>> GetAllAsync() => await _dbContext.Books.ToListAsync();

        public async Task<Book?> GetByIdAsync(int id) => await _dbContext.Books.FindAsync(id);

        public async Task<Book> AddAsync(Book book)
        {
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
            return book;
        }

        public async Task UpdateAsync(Book book)
        {
            _dbContext.Books.Update(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);
            if (book == null) return false;

            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}