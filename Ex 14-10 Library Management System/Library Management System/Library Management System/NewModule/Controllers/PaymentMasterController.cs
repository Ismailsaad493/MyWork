using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Controllers
{
    [ApiController]
    [Route("api/new-books-module/payment-master")]
    public class PaymentMasterController : ControllerBase
    {
        private readonly IPaymentMasterService _service;

        public PaymentMasterController(IPaymentMasterService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllPaymentsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PaymentCreateDto dto)
        {
            var result = await _service.CreatePaymentAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] PaymentUpdateDto dto)
        {
            var success = await _service.UpdatePaymentAsync(id, dto);
            if (!success) return NotFound($"Payment with ID {id} not found.");

            return NoContent();
        }
    }
}