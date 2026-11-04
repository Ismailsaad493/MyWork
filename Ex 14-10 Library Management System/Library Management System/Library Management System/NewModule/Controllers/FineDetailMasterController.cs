using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Controllers
{
    [Route("api/new-books-module/[controller]")]
    [ApiController]
    public class FineDetailMasterController : ControllerBase
    {
        private readonly IFineDetailMasterService _service;
        public FineDetailMasterController(IFineDetailMasterService service) { _service = service; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try { return Ok(await _service.GetAllDetailsAsync()); }
            catch (Exception) { return BadRequest(new { Message = "An error occurred while processing your request." }); }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FineDetailCreateDto dto)
        {
            if (dto.ChargeType != 1 && dto.ChargeType != 2)
            {
                return BadRequest(new { Message = "type only 1 or 2" });
            }

            try
            {
                var result = await _service.CreateDetailAsync(dto);
                if (result == null)
                {
                    return BadRequest(new { Message = "Failed to create fine detail record." });
                }
                    return CreatedAtAction(nameof(GetAll), new { id = result.FineDetailId }, result);
            }
            catch (Exception ex)
            {
                // THIS IS THE IMPORTANT CHANGE:
                // It digs into the InnerException to fetch the exact SQL Server crash message
                var actualError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { Message = actualError });
            }
        }
            [HttpPut("{id}")]
            public async Task<IActionResult> Update(string id, [FromBody] FineDetailUpdateDto dto)
            {

                if (dto.ChargeType != 1 && dto.ChargeType != 2)
                {
                    return BadRequest(new { Message = "type only 1 or 2" });
                }
                try
                {
                    var success = await _service.UpdateDetailAsync(id, dto);
                    if (!success) return StatusCode(403, new { Message = "invalid id or id not found" });
                    return NoContent();
                }
                catch (Exception) { return BadRequest(new { Message = "An error occurred while processing your request." }); }
            }
        }
    } 