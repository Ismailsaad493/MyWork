using Library_Management_System.NewModule.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Repositories
{
    public interface IBookMasterRepository
    {
        Task<IEnumerable<BookMaster>> GetAllAsync(int pageNumber, int pageSize);
        Task<BookMaster?> GetByIdAsync(int id);
        Task<BookMaster> AddAsync(BookMaster book);
        Task<bool> IsbnExistsAsync(string isbn);
        Task UpdateAsync(BookMaster book);

        // Triggers IsActive = false via DbContext interceptor
        Task DeleteAsync(BookMaster book);

        // Permanently deletes from the SQL database
        Task<bool> HardDeleteAsync(int id);
    }
}