using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Controllers
{
    [ApiController]
    [Route("api/new-books-module/fine-master")]
    public class FineMasterController : ControllerBase
    {
        private readonly IFineMasterService _service;

        public FineMasterController(IFineMasterService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllFinesAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FineCreateDto dto)
        {
            var result = await _service.CreateFineAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] FineUpdateDto dto)
        {
            var success = await _service.UpdateFineAsync(id, dto);
            if (!success) return NotFound($"Fine with ID {id} not found.");

            return NoContent();
        }
    }
}