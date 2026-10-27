using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;

namespace Library_Management_System.NewModule.Services
{
    public class BookMasterService : IBookMasterService
    {
        private readonly IBookMasterRepository _repo;

        public BookMasterService(IBookMasterRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<BookMasterResponseDto>> GetAllBooksAsync()
        {
            var books = await _repo.GetAllAsync();
            return books.Select(b => new BookMasterResponseDto
            {
                BookId = b.BookId,
                Title = b.Title,
                Isbn = b.Isbn,
                CategoryName = b.Category?.CName ?? "Unknown",
                PublisherName = b.Publisher?.PName ?? "Unknown"
            }).ToList();
        }

        public async Task<BookMasterResponseDto> CreateBookAsync(BookMasterCreateDto dto)
        {
            // Business Rule: Prevent duplicate ISBNs
            if (!string.IsNullOrEmpty(dto.Isbn) && await _repo.IsbnExistsAsync(dto.Isbn))
                throw new InvalidOperationException("A book with this ISBN already exists.");

            var book = new BookMaster
            {
                Title = dto.Title,
                Isbn = dto.Isbn,
                CId = dto.CId,
                PId = dto.PId
            };

            var savedBook = await _repo.AddAsync(book);

            // Re-fetch to ensure navigation properties (Category/Publisher names) are loaded
            var completeBook = await _repo.GetByIdAsync(savedBook.BookId);

            return new BookMasterResponseDto
            {
                BookId = completeBook!.BookId,
                Title = completeBook.Title,
                Isbn = completeBook.Isbn,
                CategoryName = completeBook.Category?.CName ?? "Unknown",
                PublisherName = completeBook.Publisher?.PName ?? "Unknown"
            };
        }
    }
}