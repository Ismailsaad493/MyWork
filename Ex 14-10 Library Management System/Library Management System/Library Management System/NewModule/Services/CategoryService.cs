using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryMaster>> GetAllCategoriesAsync(int pageNumber);
        Task<CategoryMaster> CreateCategoryAsync(CategoryCreateDto dto);
        Task<bool> UpdateCategoryAsync(int id, CategoryUpdateDto dto);
        Task<bool> DeleteCategoryAsync(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CategoryMaster>> GetAllCategoriesAsync(int pageNumber)
        {
            int pageSize = 10;
            return await _repo.GetAllAsync(pageNumber, pageSize);
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
            var existingCategory = await _repo.GetByIdAsync(id);
            if (existingCategory == null) return false;

            existingCategory.CName = dto.CName;
            await _repo.UpdateAsync(existingCategory);
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null) return false;

            await _repo.DeleteAsync(category);
            return true;
        }
    }
}