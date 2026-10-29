using Library_Management_System.NewModule.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Repositories
{
    public interface IBookMasterRepository
    {
        Task<IEnumerable<BookMaster>> GetAllAsync();
        Task<BookMaster?> GetByIdAsync(int id);
        Task<BookMaster> AddAsync(BookMaster book);
        Task<bool> IsbnExistsAsync(string isbn);
        Task UpdateAsync(BookMaster book);
    }
}