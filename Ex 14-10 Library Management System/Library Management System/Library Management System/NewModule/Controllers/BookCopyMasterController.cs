using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Controllers
{
    [ApiController]
    [Route("api/new-books-module/book-copy")]
    public class BookCopyController : ControllerBase
    {
        private readonly IBookCopyService _service;

        public BookCopyController(IBookCopyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await _service.GetAllBookCopiesAsync(pageNumber, pageSize));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookCopyCreateDto dto)
        {
            if (dto.Status != 1 && dto.Status != 2)
            {
                return BadRequest(new { Message = "type only 1 or 2" });
            }
            try
            {
                var result = await _service.CreateBookCopyAsync(dto);
                return CreatedAtAction(nameof(GetAll), new { id = result.BookCopyId }, result);
            }
            catch (Exception ex)
            {
                var actualError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookCopyUpdateDto dto)
        {
            if (dto.Status != 1 && dto.Status != 2)
            {
                return BadRequest(new { Message = "type only 1 or 2" });
            }
            try
            {
                var result = await _service.UpdateBookCopyAsync(id, dto);

                if (result == null)
                {
                    return StatusCode(403, new { Message = "invalid id or id not found" });
                }

                // Map the raw entity to your clean DTO
                var cleanResponse = new BookCopyResponseDto
                {
                    BookCopyId = result.BookCopyId,
                    BookId = result.BookId,
                    BookTitle = "Title will populate on GET",
                    Status = result.Status
                };

                return Ok(cleanResponse);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "An error occurred while processing your request." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _service.DeleteBookCopyAsync(id);
                if (!success)
                {
                    return StatusCode(403, new { Message = "invalid id or id not found" });
                }

                return Ok(new { Message = "Book copy deleted successfully." });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "An error occurred while processing your request." });
            }
        }
    }
}