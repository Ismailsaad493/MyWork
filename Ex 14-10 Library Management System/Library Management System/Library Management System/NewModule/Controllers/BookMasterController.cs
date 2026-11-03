using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Controllers
{
    [ApiController]
    [Route("api/new-books-module/book-master")]
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
            try
            {
                return Ok(await _service.GetAllBooksAsync());
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "An error occurred while processing your request." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookMasterCreateDto dto)
        {
            try
            {
                var result = await _service.CreateBookAsync(dto);
                if (result == null)
                {
                    return StatusCode(403, new { Message = "invalid id or id not found" });
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "An error occurred while processing your request." });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookMasterUpdateDto dto)
        {
            try
            {
                var success = await _service.UpdateBookAsync(id, dto);
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