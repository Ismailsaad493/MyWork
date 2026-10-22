using Library_Management_System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FinesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FinesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Fines/user/5 (View a student's fines)
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserFines(int userId)
        {
            var fines = await _context.Fines
                .Where(f => f.UserId == userId)
                .Select(f => new
                {
                    FineId = f.Id,
                    LoanId = f.BookLoanId,
                    Amount = f.Amount,
                    Status = f.IsPaid ? "Paid" : "Pending",
                    PaidOn = f.PaymentDate
                })
                .ToListAsync();

            if (!fines.Any()) return Ok(new { Message = "No fines found for this user." });

            return Ok(fines);
        }

        // POST: api/Fines/pay/2 (Pay a specific fine)
        [HttpPost("pay/{fineId}")]
        public async Task<IActionResult> PayFine(int fineId)
        {
            var fine = await _context.Fines.FindAsync(fineId);

            if (fine == null) return NotFound(new { Message = "Fine record not found." });
            if (fine.IsPaid) return BadRequest(new { Message = "This fine has already been paid." });

            fine.IsPaid = true;
            fine.PaymentDate = DateTime.Now;

            _context.Fines.Update(fine);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Fine of ₹{fine.Amount} successfully paid on {fine.PaymentDate}." });
        }
    }
}