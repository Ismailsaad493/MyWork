using LibraryApi.Infrastructure.Entity;
using LibraryApi.Infrastructure.Models;

namespace LibraryApi.Infrastructure.Repositories;

public interface IFineRepository
{
    Task<IReadOnlyList<FineRecord>> GetAllAsync(CancellationToken cancellationToken);
    Task<FineRecord?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Fine?> GetTrackedByIdAsync(int id, CancellationToken cancellationToken);
    Task<FineLoanReference?> GetLatestLoanForBookAsync(int bookId, CancellationToken cancellationToken);
    Task<bool> ExistsForLoanAsync(int bookLoanId, FineType fineType, CancellationToken cancellationToken);
    Task AddAsync(Fine fine, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
