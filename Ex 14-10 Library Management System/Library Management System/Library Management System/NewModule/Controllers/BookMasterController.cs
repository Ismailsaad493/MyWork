using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.NewModule.Controllers
{
    [ApiController]
    [Route("api/new-books-module/books")]
    public class BookMasterController : ControllerBase
    {
        private readonly IBookMasterService _service;

        public BookMasterController(IBookMasterService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _service.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookMasterCreateDto dto)
        {
            try
            {
                var result = await _service.CreateBookAsync(dto);
                return CreatedAtAction(nameof(GetAll), new { id = result.BookId }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}