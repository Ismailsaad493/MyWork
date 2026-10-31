using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Controllers
{
    [ApiController]
    [Route("api/new-books-module/book-search")]
    public class BookSearchController : ControllerBase
    {
        private readonly IBookSearchService _service;

        public BookSearchController(IBookSearchService service)
        {
            _service = service;
        }

        [HttpGet("availability")]
        public async Task<IActionResult> GetAllAvailability()
        {
            return Ok(await _service.GetAllAvailabilityAsync());
        }

        [HttpGet("search/{title}")]
        public async Task<IActionResult> SearchByTitle(string title)
        {
            var results = await _service.SearchByTitleAsync(title);
            return Ok(results);
        }
    }
}