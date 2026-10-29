using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Controllers
{
    [ApiController]
    [Route("api/new-books-module/return-master")]
    public class ReturnMasterController : ControllerBase
    {
        private readonly IReturnService _service;

        public ReturnMasterController(IReturnService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllReturnsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReturnCreateDto dto)
        {
            var result = await _service.CreateReturnAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ReturnUpdateDto dto)
        {
            var success = await _service.UpdateReturnAsync(id, dto);
            if (!success) return NotFound($"Return with ID {id} not found.");

            return NoContent();
        }
    }
}