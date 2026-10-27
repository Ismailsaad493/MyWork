using Library_Management_System.Entities;

namespace Library_Management_System.Service
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<IEnumerable<Book>> SearchBooksAsync(string? title, string? author);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
    }
}