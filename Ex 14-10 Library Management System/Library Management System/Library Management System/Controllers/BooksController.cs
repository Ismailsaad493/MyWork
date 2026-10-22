using Library_Management_System.Entities;
using Library_Management_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library_Management_System.Controllers
{
    [Authorize] // <-- 1. THIS LOCKS THE ENTIRE CONTROLLER. You MUST be logged in to do anything here.
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService; 

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/Books (Any logged-in user can view books)
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        // GET: api/Books/5 (Any logged-in user can view a specific book)
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound(new { Message = "Book not found." });

            return Ok(book);
        }

        // GET: api/Books/search (Any logged-in user can search)
        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks([FromQuery] string? title, [FromQuery] string? author, [FromQuery] string? isbn, [FromQuery] string? category)
        {
            var books = await _bookService.SearchBooksAsync(title, author, isbn, category);
            if (!books.Any()) return NotFound(new { Message = "No books found matching your search criteria." });
            return Ok(books);
        }

        // POST: api/Books 
        // 2. ROLE-BASED ACCESS: Only Admins and Librarians can add books!
        [Authorize(Roles = "Admin, Librarian")]
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            // Extract the username from the JWT token
            var username = User.FindFirst(ClaimTypes.Name)?.Value ?? "Unknown User";

            await _bookService.AddBookAsync(book);

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, new
            {
                Message = $"Book '{book.Title}' was successfully added by {username}!",
                BookDetails = book
            });
        }

        // PUT: api/Books/5
        // ROLE-BASED ACCESS: Only Admins and Librarians can edit books!
        [Authorize(Roles = "Admin, Librarian")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            if (id != book.Id) return BadRequest(new { Message = "ID mismatch." });

            var existingBook = await _bookService.GetBookByIdAsync(id);
            if (existingBook == null) return NotFound(new { Message = "Book not found." });

            // Extract the username from the JWT token
            var username = User.FindFirst(ClaimTypes.Name)?.Value ?? "Unknown User";

            await _bookService.UpdateBookAsync(book);

            return Ok(new { Message = $"Book ID {id} was successfully updated by {username}!" });
        }

        // DELETE: api/Books/5
        // ROLE-BASED ACCESS: Only Admins and Librarians can delete books!
        [Authorize(Roles = "Admin, Librarian")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var existingBook = await _bookService.GetBookByIdAsync(id);
            if (existingBook == null) return NotFound(new { Message = "Book not found." });

            // Extract the username from the JWT token
            var username = User.FindFirst(ClaimTypes.Name)?.Value ?? "Unknown User";

            await _bookService.DeleteBookAsync(id);

            return Ok(new { Message = $"Book '{existingBook.Title}' was permanently deleted by {username}!" });
        }
    }
}