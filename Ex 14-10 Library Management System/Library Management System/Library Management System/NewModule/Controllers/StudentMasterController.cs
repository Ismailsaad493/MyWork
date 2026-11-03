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
            try
            {
                var success = await _service.UpdateStudentAsync(id, dto);
                if (!success)
                {
                    return StatusCode(403, new { Message = "invalid id or id not found" });
                }

                return NoContent(); // Or Ok(new { Message = "Updated successfully" }) depending on your preference
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "An error occurred while processing your request." });
            }
        }

        [HttpDelete("{sId}")]
        public async Task<IActionResult> DeleteStudent(string sId)
        {
            try
            {
                var success = await _service.DeleteStudentAsync(sId);
                if (!success)
                {
                    return StatusCode(403, new { Message = "invalid id or id not found" });
                }

                return Ok(new { Message = "Student status successfully changed to Inactive (false)." });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "An error occurred while processing your request." });
            }
        }
    }
}