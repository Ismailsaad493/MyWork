using Library_Management_System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Controllers
{
    // Restrict reports to Admins and Librarians only
    [Authorize(Roles = "Admin, Librarian")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        // 1. DASHBOARD SUMMARY: Total books, users, active issues, returns, and overdue books
        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboardSummary()
        {
            var totalBooks = await _context.Books.SumAsync(b => (int?)b.Quantity) ?? 0;
            var totalUsers = await _context.Users.CountAsync();

            var issuedBooks = await _context.BookLoans.CountAsync(l => !l.IsReturned);
            var returnedBooks = await _context.BookLoans.CountAsync(l => l.IsReturned);

            var overdueBooks = await _context.BookLoans
                .CountAsync(l => !l.IsReturned && l.DueDate < DateTime.Now);

            return Ok(new
            {
                TotalBookStock = totalBooks,
                TotalRegisteredUsers = totalUsers,
                CurrentlyIssuedBooks = issuedBooks,
                TotalReturnedBooks = returnedBooks,
                CurrentlyOverdueBooks = overdueBooks
            });
        }

        // 2. FINE COLLECTION REPORT: Track pending, paid, and total collected fines
        [HttpGet("fines")]
        public async Task<IActionResult> GetFineCollectionReport()
        {
            var totalFines = await _context.Fines.SumAsync(f => (decimal?)f.Amount) ?? 0;
            var paidFines = await _context.Fines.Where(f => f.IsPaid).SumAsync(f => (decimal?)f.Amount) ?? 0;
            var pendingFines = await _context.Fines.Where(f => !f.IsPaid).SumAsync(f => (decimal?)f.Amount) ?? 0;

            var detailedFines = await _context.Fines
                .Include(f => f.Student)
                .Select(f => new
                {
                    FineId = f.Id,
                    Username = f.Student != null ? f.Student.Name : "Unknown",
                    Amount = f.Amount,
                    Status = f.IsPaid ? "Paid" : "Pending",
                    PaymentDate = f.PaymentDate
                })
                .ToListAsync();

            return Ok(new
            {
                Summary = new
                {
                    TotalFineGenerated = totalFines,
                    TotalCollected = paidFines,
                    TotalPending = pendingFines
                },
                FineRecords = detailedFines
            });
        }

        // 3. BORROWING REPORT: Complete log of all issue and return transactions
        [HttpGet("borrowings")]
        public async Task<IActionResult> GetBorrowingReport()
        {
            var borrowings = await _context.BookLoans
                .Include(l => l.Book)
                .Include(l => l.Student)
                .Select(l => new
                {
                    LoanId = l.Id,
                    BookTitle = l.Book != null ? l.Book.Title : "Unknown",
                    Username = l.Student != null ? l.Student.Name : "Unknown",
                    LoanDate = l.LoanDate,
                    DueDate = l.DueDate,
                    ReturnDate = l.ReturnDate,
                    Status = l.IsReturned ? "Returned" : (l.DueDate < DateTime.Now ? "Overdue" : "Active"),
                    ProcessedByStaff = l.LastModifiedBy ?? "System"
                })
                .ToListAsync();

            return Ok(borrowings);
        }
    }
}


