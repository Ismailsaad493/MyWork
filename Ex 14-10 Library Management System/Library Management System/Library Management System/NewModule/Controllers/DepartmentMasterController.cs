using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Controllers
{
    [ApiController]
    [Route("api/new-books-module/department-master")]
    public class DepartmentMasterController : ControllerBase
    {
        private readonly IDepartmentService _service;
        public DepartmentMasterController(IDepartmentService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllDepartmentsAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentUpdateDto dto)
        {
            var result = await _service.CreateDepartmentAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = result.DId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] DepartmentUpdateDto dto)
        {
            var success = await _service.UpdateDepartmentAsync(id, dto);
            if (!success) return NotFound($"Department with ID {id} not found.");
            return NoContent();
        }
    }
}