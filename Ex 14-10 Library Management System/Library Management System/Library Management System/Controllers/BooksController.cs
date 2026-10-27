using Library_Management_System.Entities;
using Library_Management_System.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library_Management_System.Controllers
{
    // REMOVED the global [Authorize] attribute here so we can control each endpoint individually
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // 🔓 Requirement 2: Students can view the catalog without a token
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        // 🔓 Requirement 2: Students can view specific book details without a token
        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound(new { Message = "Book not found." });
            return Ok(book);
        }

        // 🔓 Requirement 2: Students can search without a token
        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks([FromQuery] string? title, [FromQuery] string? author)
        {
            var books = await _bookService.SearchBooksAsync(title, author);
            if (!books.Any()) return NotFound(new { Message = "No books found matching your search criteria." });
            return Ok(books);
        }

        // 🔒 Requirement 1: Only an Admin with a valid JWT token can add books
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value ?? "Unknown User";
            await _bookService.AddBookAsync(book);

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, new
            {
                Message = $"Book '{book.Title}' was successfully added by {username}!",
                BookDetails = book
            });
        }

        // 🔒 Requirement 1: Only an Admin can update books
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            if (id != book.Id) return BadRequest(new { Message = "ID mismatch." });

            var existingBook = await _bookService.GetBookByIdAsync(id);
            if (existingBook == null) return NotFound(new { Message = "Book not found." });

            var username = User.FindFirst(ClaimTypes.Name)?.Value ?? "Unknown User";
            await _bookService.UpdateBookAsync(book);

            return Ok(new { Message = $"Book ID {id} was successfully updated by {username}!" });
        }

        // 🔒 Requirement 1: Only an Admin can delete books
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var existingBook = await _bookService.GetBookByIdAsync(id);
            if (existingBook == null) return NotFound(new { Message = "Book not found." });

            var username = User.FindFirst(ClaimTypes.Name)?.Value ?? "Unknown User";
            await _bookService.DeleteBookAsync(id);

            return Ok(new { Message = $"Book '{existingBook.Title}' was permanently deleted by {username}!" });
        }
    }
}