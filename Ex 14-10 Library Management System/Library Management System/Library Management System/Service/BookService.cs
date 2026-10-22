using Library_Management_System.Entities;
using Library_Management_System.Repositories;

namespace Library_Management_System.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllBooksAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetBookByIdAsync(id);
        }

        public async Task AddBookAsync(Book book)
        {
            // Business logic: Update availability based on quantity
            book.IsAvailable = book.Quantity > 0;
            await _bookRepository.AddBookAsync(book);
        }

        public async Task UpdateBookAsync(Book book)
        {
            book.IsAvailable = book.Quantity > 0;
            await _bookRepository.UpdateBookAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            await _bookRepository.DeleteBookAsync(id);
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string? title, string? author, string? isbn, string? category)
        {
            return await _bookRepository.SearchBooksAsync(title, author, isbn, category);
        }
    }
}