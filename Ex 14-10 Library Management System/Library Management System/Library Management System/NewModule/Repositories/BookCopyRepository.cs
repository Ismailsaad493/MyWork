using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.NewModule.Repositories
{
    public interface IBookCopyRepository
    {
        Task<IEnumerable<BookCopy>> GetAllAsync();
        Task<BookCopy> AddAsync(BookCopy copy);
    }

    public class BookCopyRepository : IBookCopyRepository
    {
        private readonly NewBooksDbContext _context;
        public BookCopyRepository(NewBooksDbContext context) => _context = context;

        public async Task<IEnumerable<BookCopy>> GetAllAsync()
        {
            return await _context.BookCopies.Include(bc => bc.Book).ToListAsync();
        }

        public async Task<BookCopy> AddAsync(BookCopy copy)
        {
            await _context.BookCopies.AddAsync(copy);
            await _context.SaveChangesAsync();
            return copy;
        }
    }
}