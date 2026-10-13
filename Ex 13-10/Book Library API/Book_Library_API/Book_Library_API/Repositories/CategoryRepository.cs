using Book_Library_API.Data;
using Book_Library_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Book_Library_API.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> AddAsync(Category category);
    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _dbContext;

        public CategoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> GetAllAsync() => await _dbContext.Categories.ToListAsync();

        public async Task<Category> AddAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }
    }
}