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

        public CategoryMasterController(ICategoryService service)
        {
            _service = service;
        }

        // --- GET WITH PAGINATION ---
        [HttpGet("{Pagenumber-Pagesize}")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1)
        {
            try
            {
                var categories = await _service.GetAllCategoriesAsync(pageNumber);
                return Ok(categories);
            }
            catch (Exception ex)
            {
                var actualError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { Message = actualError });
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
                    return StatusCode(403, new { Message = "invalid data" });
                }
                return CreatedAtAction(nameof(GetAll), new { id = result.CId }, result);
            }
            catch (Exception ex)
            {
                var actualError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { Message = actualError });
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
                    return StatusCode(403, new { Message = "invalid id or category not found" });
                }

                return Ok(new
                {
                    Success = true,
                    Message = "Category updated successfully."
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
                var success = await _service.DeleteCategoryAsync(id);
                if (!success)
                {
                    return StatusCode(403, new { Message = "invalid id or category not found" });
                }
                return Ok(new { Message = "Category deactivated successfully." });
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
                var success = await _service.HardDeleteCategoryAsync(id);
                if (!success)
                {
                    return StatusCode(403, new { Message = "invalid id or category not found" });
                }

                return Ok(new { Message = "Category permanently deleted from the database." });
            }
            catch (Exception ex)
            {
                var actualError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { Message = actualError });
            }
        }
    }
}