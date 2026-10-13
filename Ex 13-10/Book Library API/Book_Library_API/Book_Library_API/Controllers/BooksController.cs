using Book_Library_API.Entities;
using Book_Library_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace Book_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddBookDto dto)
        {
            var createdBook = await _bookService.CreateBookAsync(dto);
            return Created($"/api/books/{createdBook.Id}", createdBook);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateBookDto dto)
        {
            var updatedBook = await _bookService.UpdateBookAsync(id, dto);
            if (updatedBook == null) return NotFound();
            return Ok(updatedBook);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _bookService.DeleteBookAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}