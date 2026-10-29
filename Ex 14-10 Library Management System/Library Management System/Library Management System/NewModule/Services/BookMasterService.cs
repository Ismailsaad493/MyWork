using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
  
    // ==========================================
    // 2. THE IMPLEMENTATION
    // ==========================================
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

            var completeBook = await _repo.GetByIdAsync(savedBook.BookId);

            if (completeBook == null)
            {
                return new BookMasterResponseDto
                {
                    BookId = savedBook.BookId,
                    Title = savedBook.Title,
                    Isbn = savedBook.Isbn,
                    CategoryName = "Unknown (Inactive)",
                    PublisherName = "Unknown (Inactive)"
                };
            }

            return new BookMasterResponseDto
            {
                BookId = completeBook.BookId,
                Title = completeBook.Title,
                Isbn = completeBook.Isbn,
                CategoryName = completeBook.Category?.CName ?? "Unknown",
                PublisherName = completeBook.Publisher?.PName ?? "Unknown"
            };
        }

        public async Task<bool> UpdateBookAsync(int id, BookMasterUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            // Optional: Check if the new ISBN belongs to a different book
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
    }
}