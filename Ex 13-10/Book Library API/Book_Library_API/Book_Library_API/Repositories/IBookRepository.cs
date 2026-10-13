using Book_Library_API.Entities;

namespace Book_Library_API.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<Book> AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task<bool> DeleteAsync(int id);
    }
}