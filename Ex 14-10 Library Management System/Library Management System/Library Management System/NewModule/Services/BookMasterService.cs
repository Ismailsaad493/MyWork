using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    public class BookMasterService : IBookMasterService
    {
        private readonly IBookMasterRepository _repo;

        public BookMasterService(IBookMasterRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<BookMasterResponseDto>> GetAllBooksAsync(int pageNumber, int pageSize)
        {
            var books = await _repo.GetAllAsync(pageNumber, pageSize);

            return books.Select(b => new BookMasterResponseDto
            {
                BookId = b.BookId,
                Title = b.Title,
                CId = b.CId,
                PId = b.PId,
                Isbn = b.Isbn,
                Copies = b.Copies?.Select(c => new BookCopyDetailDto
                {
                    BookCopyId = c.BookCopyId,
                    BookId = c.BookId,
                    Status = c.Status
                }).ToList() ?? new List<BookCopyDetailDto>()
            }).ToList();
        }

        public async Task<BookMasterResponseDto> CreateBookAsync(BookMasterResponseDto dto)
        {
            if (!string.IsNullOrEmpty(dto.Isbn) && await _repo.IsbnExistsAsync(dto.Isbn))
                throw new InvalidOperationException("A book with this ISBN already exists.");

            var book = new BookMaster
            {
                Title = dto.Title,
                Isbn = dto.Isbn,
                CId = dto.CId,
                PId = dto.PId,
                // FIX: Map the nested copies so Entity Framework saves them
                Copies = dto.Copies?.Select(c => new BookCopy
                {
                    // BookCopyId is excluded here assuming the database auto-generates it
                    Status = c.Status
                }).ToList() ?? new List<BookCopy>()
            };

            var savedBook = await _repo.AddAsync(book);
            var completeBook = await _repo.GetByIdAsync(savedBook.BookId);

            if (completeBook == null)
            {
                return new BookMasterResponseDto
                {
                    BookId = savedBook.BookId,
                    Title = savedBook.Title,
                    CId = savedBook.CId,
                    PId = savedBook.PId,
                    Isbn = savedBook.Isbn
                };
            }

            return new BookMasterResponseDto
            {
                BookId = completeBook.BookId,
                Title = completeBook.Title,
                CId = completeBook.CId,
                PId = completeBook.PId,
                Isbn = completeBook.Isbn,
                Copies = completeBook.Copies?.Select(c => new BookCopyDetailDto
                {
                    BookCopyId = c.BookCopyId,
                    BookId = c.BookId,
                    Status = c.Status
                }).ToList() ?? new List<BookCopyDetailDto>()
            };
        }

        public async Task<bool> UpdateBookAsync(int id, BookMasterUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            if (!string.IsNullOrWhiteSpace(dto.Isbn) && dto.Isbn != existing.Isbn)
            {
                if (await _repo.IsbnExistsAsync(dto.Isbn))
                    throw new InvalidOperationException("Another book with this ISBN already exists.");
            }

            existing.Title = dto.Title;
            existing.Isbn = dto.Isbn?.Trim();
            existing.CId = dto.CId;
            existing.PId = dto.PId;

            await _repo.UpdateAsync(existing);
            return true;
        }

        // --- SOFT DELETE ---
        public async Task<bool> DeleteBookAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            await _repo.DeleteAsync(existing);
            return true;
        }

        // --- HARD DELETE ---
        public async Task<bool> HardDeleteBookAsync(int id)
        {
            return await _repo.HardDeleteAsync(id);
        }
    }
}