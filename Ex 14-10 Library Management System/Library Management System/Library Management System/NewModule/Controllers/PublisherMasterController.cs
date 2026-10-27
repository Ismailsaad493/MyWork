using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.NewModule.Controllers
{
    [ApiController]
    [Route("api/new-books-module/publishers")]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _service;
        public PublisherController(IPublisherService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllPublishersAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PublisherCreateDto dto)
        {
            var result = await _service.CreatePublisherAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = result.PId }, result);
        }
    }
}