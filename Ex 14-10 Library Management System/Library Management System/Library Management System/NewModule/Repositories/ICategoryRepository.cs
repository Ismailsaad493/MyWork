using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryMaster>> GetAllAsync(int pageNumber, int pageSize);
        Task<CategoryMaster?> GetByIdAsync(int id);
        Task<CategoryMaster> AddAsync(CategoryMaster category);
        Task UpdateAsync(CategoryMaster category);
        Task DeleteAsync(CategoryMaster category);
        Task<bool> HardDeleteAsync(int id);
    }

}
  