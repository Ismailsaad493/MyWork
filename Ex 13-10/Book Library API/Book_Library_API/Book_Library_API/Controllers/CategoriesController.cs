using Book_Library_API.Entities;
using Book_Library_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace Book_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCategoryDto dto)
        {
            var createdCategory = await _categoryService.CreateCategoryAsync(dto);
            return Ok(createdCategory);
        }
    }
}