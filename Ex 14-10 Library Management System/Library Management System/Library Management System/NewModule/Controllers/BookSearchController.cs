using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
            try
            {
                var results = await _service.GetAllAvailabilityAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "An error occurred while processing your request." });
            }
        }

        [HttpGet("search/{title}")]
        public async Task<IActionResult> SearchByTitle(string title)
        {
            try
            {
                var results = await _service.SearchByTitleAsync(title);

                // If no books match the search title, it triggers the 403 error
                if (results == null || !results.Any())
                {
                    return StatusCode(403, new { Message = "invalid id or id not found" });
                }

                return Ok(results);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "An error occurred while processing your request." });
            }
        }
    }
}