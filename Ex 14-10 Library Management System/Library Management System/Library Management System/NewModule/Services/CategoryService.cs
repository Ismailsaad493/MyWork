using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    // ==========================================
    // 1. THE INTERFACE (Contract)
    // ==========================================
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryMaster>> GetAllCategoriesAsync();
        Task<CategoryMaster> CreateCategoryAsync(CategoryCreateDto dto);

        // FIXED: This line resolves the red squiggly error in your controller
        Task<bool> UpdateCategoryAsync(int id, CategoryUpdateDto dto);
    }

    // ==========================================
    // 2. THE IMPLEMENTATION
    // ==========================================
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CategoryMaster>> GetAllCategoriesAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<CategoryMaster> CreateCategoryAsync(CategoryCreateDto dto)
        {
            var category = new CategoryMaster
            {
                CName = dto.CName
            };

            return await _repo.AddAsync(category);
        }

        public async Task<bool> UpdateCategoryAsync(int id, CategoryUpdateDto dto)
        {
            // 1. Fetch the existing record from the database
            var existingCategory = await _repo.GetByIdAsync(id);
            if (existingCategory == null) return false;

            // 2. Apply the new values from the DTO
            existingCategory.CName = dto.CName;

            // 3. Save the changes to the database
            await _repo.UpdateAsync(existingCategory);

            return true;
        }
    }
}