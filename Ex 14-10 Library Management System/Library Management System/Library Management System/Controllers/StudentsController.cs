using Library_Management_System.Entities;
using Library_Management_System.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    // 🔒 Requirement 1: Only an Admin with a valid JWT token can access these endpoints
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] StudentDto dto)
        {
            var student = await _studentService.RegisterStudentAsync(dto);
            return Ok(new { Message = "Student registered successfully.", StudentId = student.Id });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound(new { Message = "Student not found." });
            return Ok(student);
        }

        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetBorrowingHistory(int id)
        {
            var history = await _studentService.GetStudentHistoryAsync(id);
            if (!history.Any()) return NotFound(new { Message = "No borrowing history found for this student." });
            return Ok(history);
        }
    }
}