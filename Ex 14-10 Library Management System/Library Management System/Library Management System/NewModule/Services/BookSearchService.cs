using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    public interface IBookSearchService
    {
        Task<IEnumerable<BookAvailabilityDto>> GetAllAvailabilityAsync();
        Task<IEnumerable<BookAvailabilityDto>> SearchByTitleAsync(string title);
    }

    public class BookSearchService : IBookSearchService
    {
        private readonly NewBooksDbContext _context;

        public BookSearchService(NewBooksDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookAvailabilityDto>> GetAllAvailabilityAsync()
        {
            return await _context.Books
                .Select(b => new BookAvailabilityDto
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    TotalCopies = b.Copies.Count(),
                    // Assuming your BookCopy status strings are "Available" and "Issued"
                    AvailableCopies = b.Copies.Count(c => c.Status.ToLower() == "available"),
                    IssuedCopies = b.Copies.Count(c => c.Status.ToLower() == "issued")
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<BookAvailabilityDto>> SearchByTitleAsync(string title)
        {
            return await _context.Books
                .Where(b => b.Title.Contains(title))
                .Select(b => new BookAvailabilityDto
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    TotalCopies = b.Copies.Count(),
                    AvailableCopies = b.Copies.Count(c => c.Status.ToLower() == "available"),
                    IssuedCopies = b.Copies.Count(c => c.Status.ToLower() == "issued")
                })
                .ToListAsync();
        }
    }
}