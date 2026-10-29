using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    // The Interface
    public interface IBookCopyService
    {
        Task<IEnumerable<BookCopy>> GetAllCopiesAsync();

        // Note: Assuming you have a CreateDto with BookId. If not, use BookCopyUpdateDto and add BookId to it.
        Task<BookCopy> CreateCopyAsync(int bookId, string status);
        Task<bool> UpdateCopyAsync(int id, BookCopyUpdateDto dto);
    }

    // The Implementation
    public class BookCopyService : IBookCopyService
    {
        private readonly IBookCopyRepository _repo;

        public BookCopyService(IBookCopyRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<BookCopy>> GetAllCopiesAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<BookCopy> CreateCopyAsync(int bookId, string status)
        {
            var copy = new BookCopy
            {
                BookId = bookId,
                Status = status
            };

            return await _repo.AddAsync(copy);
        }

        public async Task<bool> UpdateCopyAsync(int id, BookCopyUpdateDto dto)
        {
            var existingCopy = await _repo.GetByIdAsync(id);
            if (existingCopy == null) return false;

            existingCopy.Status = dto.Status;

            await _repo.UpdateAsync(existingCopy);
            return true;
        }
    }
}