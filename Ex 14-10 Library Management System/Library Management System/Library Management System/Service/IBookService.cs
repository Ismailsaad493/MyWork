using Library_Management_System.Entities;

namespace Library_Management_System.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
        Task<IEnumerable<Book>> SearchBooksAsync(string? title, string? author, string? isbn, string? category);
    }
}


/*getpage
    login get id and user name 
    need show who changed the id */
