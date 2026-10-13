using Book_Library_API.Entities;
using Book_Library_API.Repositories;

namespace Book_Library_API.Service
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BookResponseDto>> GetAllBooksAsync()
        {
            var books = await _repository.GetAllAsync();
            return books.Select(b => new BookResponseDto
            {
                Id = b.Id,
                Title = b.Title,
                PublishedYear = b.PublishedYear,
                IsAvailable = b.IsAvailable,
                AuthorId = b.AuthorId,
                CategoryId = b.CategoryId
            });
        }

        public async Task<BookResponseDto?> GetBookByIdAsync(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null) return null;

            return new BookResponseDto
            {
                Id = book.Id,
                Title = book.Title,
                PublishedYear = book.PublishedYear,
                IsAvailable = book.IsAvailable,
                AuthorId = book.AuthorId,
                CategoryId = book.CategoryId
            };
        }

        public async Task<BookResponseDto> CreateBookAsync(AddBookDto dto)
        {
            var newBook = new Book
            {
                Title = dto.Title,
                PublishedYear = dto.PublishedYear,
                IsAvailable = true,
                AuthorId = dto.AuthorId,
                CategoryId = dto.CategoryId
            };

            var savedBook = await _repository.AddAsync(newBook);

            return new BookResponseDto
            {
                Id = savedBook.Id,
                Title = savedBook.Title,
                PublishedYear = savedBook.PublishedYear,
                IsAvailable = savedBook.IsAvailable,
                AuthorId = savedBook.AuthorId,
                CategoryId = savedBook.CategoryId
            };
        }

        public async Task<BookResponseDto?> UpdateBookAsync(int id, UpdateBookDto dto)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null) return null;

            book.Title = dto.Title;
            book.PublishedYear = dto.PublishedYear;
            book.IsAvailable = dto.IsAvailable;
            book.AuthorId = dto.AuthorId;
            book.CategoryId = dto.CategoryId;

            await _repository.UpdateAsync(book);

            return new BookResponseDto
            {
                Id = book.Id,
                Title = book.Title,
                PublishedYear = book.PublishedYear,
                IsAvailable = book.IsAvailable,
                AuthorId = book.AuthorId,
                CategoryId = book.CategoryId
            };
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}