using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Controllers
{
    [Route("api/new-books-module/[controller]")]
    [ApiController]
    public class PaymentMasterController : ControllerBase
    {
        private readonly IPaymentMasterService _service;

        public PaymentMasterController(IPaymentMasterService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            try { return Ok(await _service.GetAllPaymentsAsync()); }
            catch (Exception) { return BadRequest(new { Message = "An error occurred while processing your request." }); }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentCreateDto dto)
        {
            if (dto.PaymentMethod < 1 || dto.PaymentMethod > 3)
            {
                return BadRequest(new { Message = "type only 1, 2, or 3" });
            }
            try { return Ok(await _service.CreatePaymentAsync(dto)); }
            catch (Exception) { return BadRequest(new { Message = "An error occurred while processing your request." }); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(string id, [FromBody] PaymentUpdateDto dto)
        {
            if (dto.PaymentMethod < 1 || dto.PaymentMethod > 3)
            {
                return BadRequest(new { Message = "type only 1, 2, or 3" });
            }
            try
            {
                var success = await _service.UpdatePaymentAsync(id, dto);
                if (!success) return StatusCode(403, new { Message = "invalid id or id not found" });
                return NoContent();
            }
            catch (Exception) { return BadRequest(new { Message = "An error occurred while processing your request." }); }
        }
    }
}