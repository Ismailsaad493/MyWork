using LibraryApi.Dtos.Books;

namespace LibraryApi.Infrastructure.Services.Interfaces;

public interface IBookService
{
    Task<IReadOnlyList<BookResponse>> GetAllAsync(string? author, CancellationToken cancellationToken);
    Task<BookResponse> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<BookResponse> CreateAsync(CreateBookRequest request, CancellationToken cancellationToken);
    Task<BookResponse> UpdateAsync(int id, UpdateBookRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}
