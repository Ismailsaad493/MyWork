using Book_Library_API.Entities;

namespace Book_Library_API.Service
{
    public interface IBookService
    {
        Task<IEnumerable<BookResponseDto>> GetAllBooksAsync();
        Task<BookResponseDto?> GetBookByIdAsync(int id);
        Task<BookResponseDto> CreateBookAsync(AddBookDto dto);
        Task<BookResponseDto?> UpdateBookAsync(int id, UpdateBookDto dto);
        Task<bool> DeleteBookAsync(int id);
    }
}