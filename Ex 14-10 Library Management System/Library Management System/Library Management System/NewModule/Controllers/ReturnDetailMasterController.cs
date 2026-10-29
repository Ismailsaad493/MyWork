using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Controllers
{
    [ApiController]
    [Route("api/new-books-module/return-detail-master")]
    public class ReturnDetailMasterController : ControllerBase
    {
        private readonly IReturnDetailService _service;

        public ReturnDetailMasterController(IReturnDetailService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllDetailsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReturnDetailCreateDto dto)
        {
            var result = await _service.CreateDetailAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ReturnDetailUpdateDto dto)
        {
            var success = await _service.UpdateDetailAsync(id, dto);
            if (!success) return NotFound($"Return Detail with ID {id} not found.");

            return NoContent();
        }
    }
}