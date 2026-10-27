using Library_Management_System.Data;
using Library_Management_System.Entities;

namespace Library_Management_System.Service
{
    public class BookLoanService : IBookLoanService
    {
        private readonly AppDbContext _context;

        public BookLoanService(AppDbContext context)
        {
            _context = context;
        }

        // Updated to use DateTime instead of DateOnly
        private void ValidateLoanDates(DateTime loanDate, DateTime dueDate, DateTime? returnDate)
        {
            if (dueDate < loanDate) throw new ArgumentException("Due date cannot be earlier than loan date.");
            if (returnDate.HasValue && returnDate.Value < loanDate) throw new ArgumentException("Return date cannot be earlier than loan date.");
        }

        public async Task<BookLoan> BorrowBookAsync(int studentId, int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            var student = await _context.Students.FindAsync(studentId); // FIXED: Capitalized and pluralized to Students

            if (book == null || student == null) throw new Exception("Book or Student not found.");
            if (!book.IsAvailable || book.Quantity <= 0) throw new Exception("This book is currently out of stock.");

            // Updated to use DateTime
            var loanDate = DateTime.Now;
            var dueDate = loanDate.AddDays(14);

            ValidateLoanDates(loanDate, dueDate, null);

            var loan = new BookLoan
            {
                BookId = bookId,
                StudentId = studentId,
                LoanDate = loanDate,
                DueDate = dueDate
            };

            book.Quantity -= 1;
            book.IsAvailable = book.Quantity > 0;

            await _context.BookLoans.AddAsync(loan);
            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            return loan;
        }

        public async Task<BookLoan> ExtendDueDateAsync(int loanId, int extraDays, string currentStaff)
        {
            var loan = await _context.BookLoans.FindAsync(loanId);
            if (loan == null || loan.IsReturned) throw new Exception("Invalid loan record or book already returned.");

            var newDueDate = loan.DueDate.AddDays(extraDays);
            ValidateLoanDates(loan.LoanDate, newDueDate, loan.ReturnDate);

            loan.DueDate = newDueDate;
            loan.LastModifiedBy = currentStaff;
            loan.LastModifiedDate = DateTime.Now;

            _context.BookLoans.Update(loan);
            await _context.SaveChangesAsync();

            return loan;
        }

        public async Task<(bool IsLate, decimal FineAmount)> ReturnBookAsync(int loanId, string currentStaff)
        {
            var loan = await _context.BookLoans.FindAsync(loanId);
            if (loan == null || loan.IsReturned) throw new Exception("Invalid loan record or book already returned.");

            var book = await _context.Books.FindAsync(loan.BookId);
            if (book == null) throw new Exception("Associated book not found.");

            // Updated to use DateTime
            var returnDate = DateTime.Now;
            ValidateLoanDates(loan.LoanDate, loan.DueDate, returnDate);

            loan.IsReturned = true;
            loan.ReturnDate = returnDate;
            loan.LastModifiedBy = currentStaff;
            loan.LastModifiedDate = DateTime.Now;

            decimal fineAmount = 0;

            // Updated fine calculation logic to support DateTime subtraction
            var daysLate = Math.Ceiling((returnDate - loan.DueDate).TotalDays);

            if (daysLate > 0)
            {
                fineAmount = (decimal)daysLate * 10; // FIXED: Removed hyphen, added * symbol
                var fine = new Fine { BookLoanId = loan.Id, StudentId = loan.StudentId, Amount = fineAmount, IsPaid = false }; // FIXED: Changed UserId to StudentId
                await _context.Fines.AddAsync(fine);
            }

            book.Quantity += 1;
            book.IsAvailable = true;

            _context.BookLoans.Update(loan);
            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            return (daysLate > 0, fineAmount);
        }
    }
}