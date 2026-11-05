using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Controllers
{
    [ApiController]
    [Route("api/new-books-module/publisher-master")]
    public class PublisherMasterController : ControllerBase
    {
        private readonly IPublisherService _service;

        public PublisherMasterController(IPublisherService service)
        {
            _service = service;
        }

        // --- GET WITH PAGINATION ---
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1)
        {
            try
            {
                var publishers = await _service.GetAllPublishersAsync(pageNumber);
                return Ok(publishers);
            }
            catch (Exception ex)
            {
                var actualError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { Message = actualError });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PublisherCreateDto dto)
        {
            try
            {
                var result = await _service.CreatePublisherAsync(dto);
                if (result == null)
                {
                    return StatusCode(403, new { Message = "invalid data" });
                }
                return CreatedAtAction(nameof(GetAll), new { id = result.PId }, result);
            }
            catch (Exception ex)
            {
                var actualError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { Message = actualError });
            }
        }

        // --- PUT WITH PROPER RESPONSE BODY ---
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PublisherUpdateDto dto)
        {
            try
            {
                var success = await _service.UpdatePublisherAsync(id, dto);
                if (!success)
                {
                    return StatusCode(403, new { Message = "invalid id or publisher not found" });
                }

                // Returns a structured JSON response instead of an empty 200 OK
                return Ok(new
                {
                    Success = true,
                    Message = "Publisher updated successfully."
                });
            }
            catch (Exception ex)
            {
                var actualError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { Message = actualError });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _service.DeletePublisherAsync(id);
                if (!success)
                {
                    return StatusCode(403, new { Message = "invalid id or publisher not found" });
                }
                return Ok(new { Message = "Publisher deactivated successfully." });
            }
            catch (Exception ex)
            {
                var actualError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { Message = actualError });
            }
        }

        [HttpDelete("hard-delete/{id}")]
        public async Task<IActionResult> HardDelete(int id)
        {
            try
            {
                var success = await _service.HardDeletePublisherAsync(id);
                if (!success)
                {
                    return StatusCode(403, new { Message = "invalid id or publisher not found" });
                }

                return Ok(new { Message = "Publisher permanently deleted from the database." });
            }
            catch (Exception ex)
            {
                var actualError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { Message = actualError });
            }
        }
    }
}