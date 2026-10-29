using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Controllers
{
    [ApiController]
    [Route("api/new-books-module/book-copies")]
    public class BookCopyController : ControllerBase
    {
        private readonly IBookCopyService _service;

        public BookCopyController(IBookCopyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var copies = await _service.GetAllCopiesAsync();
            return Ok(copies);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int bookId, [FromBody] BookCopyUpdateDto dto)
        {
            var result = await _service.CreateCopyAsync(bookId, dto.Status);
            return CreatedAtAction(nameof(GetAll), new { id = result.BookCopyId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookCopyUpdateDto dto)
        {
            var success = await _service.UpdateCopyAsync(id, dto);
            if (!success) return NotFound($"Book Copy with ID {id} not found.");

            return NoContent();
        }
    }
}