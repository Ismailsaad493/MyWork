using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryMaster>> GetAllAsync();
        Task<CategoryMaster> AddAsync(CategoryMaster category);

        // NEW: For Update
        Task<CategoryMaster?> GetByIdAsync(int id);
        Task UpdateAsync(CategoryMaster category);
    }

}
  