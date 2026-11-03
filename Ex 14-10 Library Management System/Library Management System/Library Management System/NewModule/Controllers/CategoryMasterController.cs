using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Controllers
{
    [ApiController]
    [Route("api/new-books-module/category-master")]
    public class CategoryMasterController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoryMasterController(ICategoryService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _service.GetAllCategoriesAsync());
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "An error occurred while processing your request." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto dto)
        {
            try
            {
                var result = await _service.CreateCategoryAsync(dto);
                if (result == null)
                {
                    return StatusCode(403, new { Message = "invalid id or id not found" });
                }
                return CreatedAtAction(nameof(GetAll), new { id = result.CId }, result);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "An error occurred while processing your request." });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryUpdateDto dto)
        {
            try
            {
                var success = await _service.UpdateCategoryAsync(id, dto);
                if (!success)
                {
                    return StatusCode(403, new { Message = "invalid id or id not found" });
                }

                return NoContent(); // 204 No Content is standard for successful PUT requests
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
                // NOTE: Change 'DeleteAsync' to your actual service method name (e.g., DeleteCategoryAsync)
                var success = await _service.DeleteCategoryAsync(id); if (!success)
                {
                    return StatusCode(403, new { Message = "invalid id or id not found" });
                }
                return Ok(new { Message = "Record deleted successfully." });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "An error occurred while processing your request." });
            }
        }
    }
}