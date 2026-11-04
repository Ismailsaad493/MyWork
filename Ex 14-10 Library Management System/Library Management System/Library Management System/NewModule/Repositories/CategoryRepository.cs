using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly NewBooksDbContext _context;

        public CategoryRepository(NewBooksDbContext context) => _context = context;

        public async Task<IEnumerable<CategoryMaster>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<CategoryMaster> AddAsync(CategoryMaster category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        // For Update
        public async Task<CategoryMaster?> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task UpdateAsync(CategoryMaster category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(CategoryMaster category)
        {
            // FIX: Using Remove() allows the DbContext SaveChangesAsync override 
            // to intercept the delete and set IsActive = false instead.
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryMaster>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.Categories
                .Skip((pageNumber - 1) * pageSize) // Fixed typo here
                .Take(pageSize)                    // Takes exactly the number of records for the current page
                .ToListAsync();
        }
    }
}