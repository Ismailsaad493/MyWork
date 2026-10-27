using LibraryApi.Infrastructure.Entity;

namespace LibraryApi.Infrastructure.Repositories;

public interface ILoanRepository
{
    Task<IReadOnlyList<Loan>> GetAllAsync(CancellationToken cancellationToken);
    Task<Loan?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Loan?> GetActiveByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(Loan loan, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
