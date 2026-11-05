using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Controllers
{
    [Route("api/new-books-module/[controller]")]
    [ApiController]
    public class FineMasterController : ControllerBase
    {
        private readonly IFineMasterService _service;

        public FineMasterController(IFineMasterService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFines()
        {
            try { return Ok(await _service.GetAllFinesAsync()); }
            catch (Exception ex)
            {
                // This will reveal the exact line or database issue that is breaking
                var actualError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { Message = actualError });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateFine([FromBody] FineCreateDto dto)
        {
            if (dto.Status != 1 && dto.Status != 2)
            {
                return BadRequest(new { Message = "type only 1 or 2" });
            }
            try { return Ok(await _service.CreateFineAsync(dto)); }
            catch (Exception) { return BadRequest(new { Message = "An error occurred while processing your request." }); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFine(string id, [FromBody] FineUpdateDto dto)
        {
            if (dto.Status != 1 && dto.Status != 2)
            {
                return BadRequest(new { Message = "type only 1 or 2" });
            }
            try
            {
                var success = await _service.UpdateFineAsync(id, dto);
                if (!success) return StatusCode(403, new { Message = "invalid id or id not found" });
                return NoContent();
            }
            catch (Exception) { return BadRequest(new { Message = "An error occurred while processing your request." }); }
        }
    }
}