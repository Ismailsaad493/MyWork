using Library_Management_System.Data;
using Library_Management_System.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Users/register
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User newUser)
        {
            if (await _context.Users.AnyAsync(u => u.Username == newUser.Username))
                return BadRequest(new { Message = "Username already exists." });

            if (await _context.Users.AnyAsync(u => u.UniversityId == newUser.UniversityId))
                return BadRequest(new { Message = "Student/Faculty ID already registered." });

            // Hash the password securely using BCrypt before saving
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"{newUser.Role} registered successfully!" });
        }

        // GET: api/Users/{id}/history
        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetBorrowingHistory(int id)
        {
            // First, verify the user exists
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound(new { Message = "User not found." });

            // Fetch the loan history and join it with the Book details
            var history = await _context.BookLoans
                .Where(loan => loan.UserId == id)
                .Join(_context.Books,
                      loan => loan.BookId,
                      book => book.Id,
                      (loan, book) => new
                      {
                          LoanId = loan.Id,
                          BookTitle = book.Title,
                          Author = book.Author,
                          LoanDate = loan.LoanDate,
                          DueDate = loan.DueDate,
                          ReturnDate = loan.ReturnDate,
                          IsReturned = loan.IsReturned
                      })
                .OrderByDescending(h => h.LoanDate) // Shows newest borrows first
                .ToListAsync();

            if (!history.Any())
                return Ok(new { Message = "No borrowing history found for this user." });

            return Ok(history);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetails(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound(new { Message = "User not found." });

            // Hide the password before sending data back
            user.Password = "********";
            return Ok(user);
        }
    }
}