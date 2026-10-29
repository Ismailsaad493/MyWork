using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Controllers
{
    [ApiController] // <-- CRITICAL: Swagger needs this
    [Route("api/new-books-module/student-master")] // <-- CRITICAL: Swagger needs this
    public class StudentMasterController : ControllerBase // <-- CRITICAL: Must inherit from ControllerBase
    {
        private readonly IStudentService _service;

        public StudentMasterController(IStudentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllStudentsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentUpdateDto dto)
        {
            var result = await _service.CreateStudentAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = result.SId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] StudentUpdateDto dto)
        {
            var success = await _service.UpdateStudentAsync(id, dto);
            if (!success) return NotFound($"Student with ID {id} not found.");

            return NoContent();
        }
    }
}