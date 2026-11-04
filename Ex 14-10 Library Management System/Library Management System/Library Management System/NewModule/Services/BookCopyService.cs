using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    public interface IBookCopyService
    {
        Task<IEnumerable<BookCopyResponseDto>> GetAllBookCopiesAsync(int pageNumber, int pageSize);
        Task<BookCopyResponseDto> CreateBookCopyAsync(BookCopyCreateDto dto);
        Task<BookCopy?> UpdateBookCopyAsync(int id, BookCopyUpdateDto dto);
        Task<bool> DeleteBookCopyAsync(int id);
    }

    public class BookCopyService : IBookCopyService
    {
        private readonly IBookCopyRepository _repo;
        public BookCopyService(IBookCopyRepository repo) => _repo = repo;

        public async Task<IEnumerable<BookCopyResponseDto>> GetAllBookCopiesAsync(int pageNumber, int pageSize)
        {
            var copies = await _repo.GetAllAsync(pageNumber, pageSize);

            return copies.Select(c => new BookCopyResponseDto
            {
                BookCopyId = c.BookCopyId,
                BookId = c.BookId,
                BookTitle = c.Book?.Title ?? string.Empty,
                Status = c.Status
            }).ToList();
        }

        public async Task<BookCopyResponseDto> CreateBookCopyAsync(BookCopyCreateDto dto)
        {
            var copy = new BookCopy
            {
                BookId = dto.BookId,
                Status = dto.Status
            };

            var savedCopy = await _repo.AddAsync(copy);

            // Fetch the record back out of the DB so Entity Framework joins the Book Title
            var completeCopy = await _repo.GetByIdAsync(savedCopy.BookCopyId);

            return new BookCopyResponseDto
            {
                BookCopyId = completeCopy!.BookCopyId,
                BookId = completeCopy.BookId,
                BookTitle = completeCopy.Book?.Title ?? "Unknown",
                Status = completeCopy.Status
            };
        }

        public async Task<BookCopy?> UpdateBookCopyAsync(int id, BookCopyUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            // Updates only the status, matching your Update DTO
            existing.Status = dto.Status;

            await _repo.UpdateAsync(existing);
            return existing;
        }

        public async Task<bool> DeleteBookCopyAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            await _repo.DeleteAsync(existing);
            return true;
        }
    }
}