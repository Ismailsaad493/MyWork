using Library_Management_System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Controllers
{
    // 🔒 Requirement 1: Only Admins can manage fines
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class FinesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FinesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Fines/student/5 
        [HttpGet("student/{studentId}")] // FIXED: Changed user to student
        public async Task<IActionResult> GetStudentFines(int studentId) // FIXED: Changed userId to studentId
        {
            var fines = await _context.Fines
                .Where(f => f.StudentId == studentId) // FIXED: Changed UserId to StudentId
                .Select(f => new
                {
                    FineId = f.Id,
                    LoanId = f.BookLoanId,
                    Amount = f.Amount,
                    Status = f.IsPaid ? "Paid" : "Pending"
                    // FIXED: Removed PaymentDate since it is no longer in the Fine entity
                })
                .ToListAsync();

            if (!fines.Any()) return Ok(new { Message = "No fines found for this student." });

            return Ok(fines);
        }

        // POST: api/Fines/pay/2
        [HttpPost("pay/{fineId}")]
        public async Task<IActionResult> PayFine(int fineId)
        {
            var fine = await _context.Fines.FindAsync(fineId);

            if (fine == null) return NotFound(new { Message = "Fine record not found." });
            if (fine.IsPaid) return BadRequest(new { Message = "This fine has already been paid." });

            fine.IsPaid = true;
            // FIXED: Removed PaymentDate assignment since it no longer exists

            _context.Fines.Update(fine);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Fine of ₹{fine.Amount} successfully marked as paid." });
        }
    }
}