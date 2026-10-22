using Microsoft.AspNetCore.Authorization; 
using Library_Management_System.Data;
using Library_Management_System.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookLoansController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookLoansController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("borrow")]
        public async Task<IActionResult> BorrowBook(int userId, int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            var user = await _context.Users.FindAsync(userId);

            if (book == null || user == null)
                return NotFound(new { Message = "Book or User not found." });

            if (!book.IsAvailable || book.Quantity <= 0)
                return BadRequest(new { Message = "This book is currently out of stock." });

            var loan = new BookLoan
            {
                BookId = bookId,
                UserId = userId,
                DueDate = DateTime.Now.AddDays(14)
            };

            book.Quantity -= 1;
            book.IsAvailable = book.Quantity > 0;

            await _context.BookLoans.AddAsync(loan);
            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Book borrowed successfully!", DueDate = loan.DueDate });
        }
        // PUT: api/BookLoans/extend/{loanId}
        [Authorize(Roles = "Admin, Librarian")]
        [HttpPut("extend/{loanId}")]
        public async Task<IActionResult> ExtendDueDate(int loanId, [FromQuery] int extraDays)
        {
            var loan = await _context.BookLoans.FindAsync(loanId);

            if (loan == null || loan.IsReturned)
                return BadRequest(new { Message = "Invalid loan record or book already returned." });

            // 1. Identify EXACTLY who is making this change using their Token
            var currentStaff = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value ?? "Unknown User";

            // 2. Add the extra days to the due date
            loan.DueDate = loan.DueDate.AddDays(extraDays);

            // 3. Record the audit trail in the database
            loan.LastModifiedBy = currentStaff;
            loan.LastModifiedDate = DateTime.Now;

            _context.BookLoans.Update(loan);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = $"Due date successfully extended by {extraDays} days.",
                NewDueDate = loan.DueDate,
                ApprovedBy = currentStaff
            });
        }

        // POST: api/BookLoans/return/{loanId}
        [HttpPost("return/{loanId}")]
        public async Task<IActionResult> ReturnBook(int loanId)
        {
            var loan = await _context.BookLoans.FindAsync(loanId);
            if (loan == null || loan.IsReturned)
                return BadRequest(new { Message = "Invalid loan record or book already returned." });

            var book = await _context.Books.FindAsync(loan.BookId);
            if (book == null)
                return NotFound(new { Message = "Associated book not found." });

            // 1. Identify EXACTLY who is processing this return using their Token
            var currentStaff = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value ?? "Unknown User";

            // 2. Mark the loan as returned and record the audit trail
            loan.IsReturned = true;
            loan.ReturnDate = DateTime.Now;
            loan.LastModifiedBy = currentStaff;      // <--- This saves the name to the database!
            loan.LastModifiedDate = DateTime.Now;

            // 3. AUTOMATIC FINE CALCULATION
            decimal fineAmount = 0;
            if (loan.ReturnDate.Value > loan.DueDate)
            {
                // Calculate days late (rounded up)
                var daysLate = Math.Ceiling((loan.ReturnDate.Value - loan.DueDate).TotalDays);
                if (daysLate > 0)
                {
                    fineAmount = (decimal)daysLate * 10; // <--- FIXED: Added the * multiplication symbol here

                    var fine = new Fine
                    {
                        BookLoanId = loan.Id,
                        UserId = loan.UserId,
                        Amount = fineAmount,
                        IsPaid = false
                    };
                    await _context.Fines.AddAsync(fine);
                }
            }

            // 4. Increase the book quantity
            book.Quantity += 1;
            book.IsAvailable = true;

            // 5. Save to database
            _context.BookLoans.Update(loan);
            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            if (fineAmount > 0)
                return Ok(new { Message = $"Book returned late! A fine of ₹{fineAmount} has been added to your account." });

            return Ok(new { Message = "Book returned successfully on time!" });
        }
    }
}