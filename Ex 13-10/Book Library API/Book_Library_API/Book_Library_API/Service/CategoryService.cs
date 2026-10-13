using Book_Library_API.Entities;
using Book_Library_API.Repositories;

namespace Book_Library_API.Service
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> CreateCategoryAsync(AddCategoryDto dto);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories.Select(c => new CategoryDto { Id = c.Id, Name = c.Name });
        }

        public async Task<CategoryDto> CreateCategoryAsync(AddCategoryDto dto)
        {
            var category = new Category { Name = dto.Name };
            var savedCategory = await _repository.AddAsync(category);
            return new CategoryDto { Id = savedCategory.Id, Name = savedCategory.Name };
        }
    }
}