using LibraryApi.Infrastructure.Entity;

namespace LibraryApi.Infrastructure.Repositories;

public interface IBookRepository
{
    Task<IReadOnlyList<Book>> GetAllAsync(string? author, CancellationToken cancellationToken);
    Task<Book?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> HasLoansAsync(int bookId, CancellationToken cancellationToken);
    Task AddAsync(Book book, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task DeleteAsync(Book book, CancellationToken cancellationToken);
}
