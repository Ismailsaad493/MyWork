using Library_Management_System.Entities;
using Library_Management_System.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class FinesController : ControllerBase
    {
        private readonly IFineService _fineService;

        public FinesController(IFineService fineService)
        {
            _fineService = fineService;
        }

        // POST: api/Fines/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateFine([FromBody] CreateFineDto dto)
        {
            if (dto.FineType != "Book Damage" && dto.FineType != "Late Submission")
                return BadRequest(new { Message = "FineType must be 'Book Damage' or 'Late Submission'." });

            var result = await _fineService.CreateFineAsync(dto);
            return Ok(new { Message = "Fine created successfully.", Fine = result });
        }

        // GET: api/Fines/student/5
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetStudentFines(int studentId)
        {
            var fines = await _fineService.GetStudentFinesAsync(studentId);
            if (!fines.Any())
                return Ok(new { Message = "No fines found for this student." });

            return Ok(fines);
        }

        // POST: api/Fines/pay/2
        [HttpPost("pay/{fineId}")]
        public async Task<IActionResult> PayFine(int fineId)
        {
            var result = await _fineService.PayFineAsync(fineId);

            if (result == null)
                return NotFound(new { Message = "Fine record not found." });

            return Ok(new
            {
                Message = $"Fine successfully marked as paid on {result.PaymentDate}.",
                Fine = result
            });
        }
    }
}