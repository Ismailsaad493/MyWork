using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Repositories
{
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

        // NEW: For Update
        public async Task<CategoryMaster?> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task UpdateAsync(CategoryMaster category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}