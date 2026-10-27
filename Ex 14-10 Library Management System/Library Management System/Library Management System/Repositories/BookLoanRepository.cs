using Library_Management_System.Data;
using Library_Management_System.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Repositories
{
    public interface IBookLoanRepository
    {
        Task<BookLoan> AddAsync(BookLoan loan);
        Task<BookLoan?> GetByIdAsync(int loanId);
        Task UpdateAsync(BookLoan loan);
        Task<Book?> GetBookTrackingStockAsync(int bookId);
    }

    public class BookLoanRepository : IBookLoanRepository
    {
        private readonly AppDbContext _context;

        public BookLoanRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BookLoan> AddAsync(BookLoan loan)
        {
            await _context.BookLoans.AddAsync(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task<BookLoan?> GetByIdAsync(int loanId)
        {
            return await _context.BookLoans
                .Include(bl => bl.Book)
                    .ThenInclude(b => b!.Author)
                .Include(bl => bl.Student)
                .SingleOrDefaultAsync(bl => bl.Id == loanId);
        }

        public async Task<Book?> GetBookTrackingStockAsync(int bookId)
        {
            return await _context.Books.FindAsync(bookId);
        }

        public async Task UpdateAsync(BookLoan loan)
        {
            _context.BookLoans.Update(loan);
            await _context.SaveChangesAsync();
        }
    }
}