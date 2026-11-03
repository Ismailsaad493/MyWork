using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;
using System;
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
            try
            {
                var copies = await _service.GetAllCopiesAsync();
                return Ok(copies);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "An error occurred while processing your request." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(int bookId, [FromBody] BookCopyUpdateDto dto)
        {
            try
            {
                var result = await _service.CreateCopyAsync(bookId, dto.Status);
                if (result == null)
                {
                    return StatusCode(403, new { Message = "invalid id or id not found" });
                }
                return CreatedAtAction(nameof(GetAll), new { id = result.BookCopyId }, result);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "An error occurred while processing your request." });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookCopyUpdateDto dto)
        {
            try
            {
                var success = await _service.UpdateCopyAsync(id, dto);
                if (!success)
                {
                    return StatusCode(403, new { Message = "invalid id or id not found" });
                }

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "An error occurred while processing your request." });
            }
        }
    }
}