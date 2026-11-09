using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Controllers
{
    [ApiController]
    [Route("api/new-books-module/department-master")]
    public class DepartmentMasterController : ControllerBase
    {
        private readonly IDepartmentService _service;
        public DepartmentMasterController(IDepartmentService service) => _service = service;

        [HttpGet("{Pagenumber-Pagesize}")]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllDepartmentsAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentCreateDto dto)
        {
            try
            {
                var result = await _service.CreateDepartmentAsync(dto);
                if (result == null)
                {
                    return BadRequest(new { Message = "Bad Request: Invalid data provided." });
                }

                // FIXED: Return 200 OK instead of crashing on the missing route
                return Ok(result);
            }
            catch (Exception ex)
            {
                var actualError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { Message = actualError });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] DepartmentUpdateDto dto)
        {
            try
            {
                var result = await _service.UpdateDepartmentAsync(id, dto);

                // If the service returns null, the ID was not found
                if (result == null)
                {
                    return StatusCode(403, new { Message = "invalid id or id not found" });
                }

                // Returns a 200 OK with the full updated database record as the response body
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "An error occurred while processing your request." });
            }
        }
    }
}