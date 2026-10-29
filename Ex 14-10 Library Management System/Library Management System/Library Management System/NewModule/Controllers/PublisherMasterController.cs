using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Controllers
{
    [ApiController]
    [Route("api/new-books-module/publisher-master")]
    public class PublisherMasterController : ControllerBase
    {
        private readonly IPublisherService _service;

        public PublisherMasterController(IPublisherService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var publishers = await _service.GetAllPublishersAsync();
            return Ok(publishers);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PublisherUpdateDto dto)
        {
            var result = await _service.CreatePublisherAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = result.PId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PublisherUpdateDto dto)
        {
            var success = await _service.UpdatePublisherAsync(id, dto);
            if (!success) return NotFound($"Publisher with ID {id} not found.");

            return NoContent();
        }
    }
}