using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;

namespace Library_Management_System.NewModule.Services
{
    public interface IBookCopyService
    {
        Task<IEnumerable<BookCopyResponseDto>> GetAllCopiesAsync();
        Task<BookCopyResponseDto> CreateCopyAsync(BookCopyCreateDto dto);
    }

    public class BookCopyService : IBookCopyService
    {
        private readonly IBookCopyRepository _repo;
        private readonly IBookMasterRepository _bookRepo;

        public BookCopyService(IBookCopyRepository repo, IBookMasterRepository bookRepo)
        {
            _repo = repo;
            _bookRepo = bookRepo;
        }

        public async Task<IEnumerable<BookCopyResponseDto>> GetAllCopiesAsync()
        {
            var copies = await _repo.GetAllAsync();
            return copies.Select(c => new BookCopyResponseDto
            {
                BookCopyId = c.BookCopyId,
                BookId = c.BookId,
                BookTitle = c.Book?.Title ?? "Unknown",
                Status = c.Status
            });
        }

        public async Task<BookCopyResponseDto> CreateCopyAsync(BookCopyCreateDto dto)
        {
            // Verify the book actually exists before creating a copy
            var book = await _bookRepo.GetByIdAsync(dto.BookId);
            if (book == null) throw new InvalidOperationException($"Book with ID {dto.BookId} does not exist.");

            var copy = new BookCopy
            {
                BookId = dto.BookId,
                Status = dto.Status
            };

            var saved = await _repo.AddAsync(copy);
            return new BookCopyResponseDto
            {
                BookCopyId = saved.BookCopyId,
                BookId = saved.BookId,
                BookTitle = book.Title,
                Status = saved.Status
            };
        }
    }
}