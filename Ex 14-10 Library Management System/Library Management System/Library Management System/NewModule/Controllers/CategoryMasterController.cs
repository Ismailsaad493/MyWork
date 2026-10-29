using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllCategoriesAsync());



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto dto)
        {
            var result = await _service.CreateCategoryAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = result.CId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryUpdateDto dto)
        {
            var success = await _service.UpdateCategoryAsync(id, dto);
            if (!success) return NotFound($"Category with ID {id} not found.");

            return NoContent(); // 204 No Content is standard for successful PUT requests
        }

    }
}