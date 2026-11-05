using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    public interface ICategoryService
    {
        // Added pageNumber
        Task<IEnumerable<CategoryMaster>> GetAllCategoriesAsync(int pageNumber);
        Task<CategoryMaster> CreateCategoryAsync(CategoryCreateDto dto);
        Task<bool> UpdateCategoryAsync(int id, CategoryUpdateDto dto);
        Task<bool> DeleteCategoryAsync(int id);
        Task<bool> HardDeleteCategoryAsync(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        // Added pageNumber and pageSize
        public async Task<IEnumerable<CategoryMaster>> GetAllCategoriesAsync(int pageNumber)
        {
            int pageSize = 10;
            return await _repo.GetAllAsync(pageNumber, pageSize);
        }

        public async Task<CategoryMaster> CreateCategoryAsync(CategoryCreateDto dto)
        {
            var category = new CategoryMaster
            {
                CName = dto.CName // Assuming CName is the property
            };
            return await _repo.AddAsync(category);
        }

        public async Task<bool> UpdateCategoryAsync(int id, CategoryUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.CName = dto.CName;
            await _repo.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            await _repo.DeleteAsync(existing);
            return true;
        }

        public async Task<bool> HardDeleteCategoryAsync(int id)
        {
            return await _repo.HardDeleteAsync(id);
        }
    }
}