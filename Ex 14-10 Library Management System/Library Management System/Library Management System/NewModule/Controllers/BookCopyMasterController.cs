using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.NewModule.Controllers
{
    [ApiController]
    [Route("api/new-books-module/book-copy-master")]
    public class BookCopyMasterController : ControllerBase // FIXED: Renamed to add "Master"
    {
        private readonly IBookCopyService _service;
        public BookCopyMasterController(IBookCopyService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllCopiesAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookCopyCreateDto dto)
        {
            try
            {
                var result = await _service.CreateCopyAsync(dto);
                return CreatedAtAction(nameof(GetAll), new { id = result.BookCopyId }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}