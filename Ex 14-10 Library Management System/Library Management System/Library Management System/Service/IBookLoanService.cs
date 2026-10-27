using Library_Management_System.Entities;

namespace Library_Management_System.Service
{
    public interface IBookLoanService
    {
        Task<BookLoan> BorrowBookAsync(int userId, int bookId);
        Task<BookLoan> ExtendDueDateAsync(int loanId, int extraDays, string currentStaff);
        Task<(bool IsLate, decimal FineAmount)> ReturnBookAsync(int loanId, string currentStaff);
    }
}