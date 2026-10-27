using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.NewModule.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryMaster>> GetAllAsync();
        Task<CategoryMaster> AddAsync(CategoryMaster category);
    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly NewBooksDbContext _context;
        public CategoryRepository(NewBooksDbContext context) => _context = context;

        public async Task<IEnumerable<CategoryMaster>> GetAllAsync() => await _context.Categories.ToListAsync();

        public async Task<CategoryMaster> AddAsync(CategoryMaster category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }
    }
}