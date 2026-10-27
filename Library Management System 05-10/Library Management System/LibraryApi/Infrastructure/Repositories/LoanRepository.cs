using LibraryApi.Infrastructure.Context;
using LibraryApi.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Infrastructure.Repositories;

public sealed class LoanRepository(LibraryDbContext context) : ILoanRepository
{
    public async Task<IReadOnlyList<Loan>> GetAllAsync(CancellationToken cancellationToken) =>
        await context.Loans.AsNoTracking()
            .Include(loan => loan.Book)
            .Include(loan => loan.Member)
            .OrderByDescending(loan => loan.BorrowedAtUtc)
            .ThenByDescending(loan => loan.Id)
            .ToListAsync(cancellationToken);

    public Task<Loan?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        context.Loans.AsNoTracking()
            .Include(loan => loan.Book)
            .Include(loan => loan.Member)
            .SingleOrDefaultAsync(loan => loan.Id == id, cancellationToken);

    public Task<Loan?> GetActiveByIdAsync(int id, CancellationToken cancellationToken) =>
        context.Loans.Include(loan => loan.Book)
            .Include(loan => loan.Member)
            .SingleOrDefaultAsync(loan => loan.Id == id && loan.ReturnedAtUtc == null, cancellationToken);

    public async Task AddAsync(Loan loan, CancellationToken cancellationToken)
    {
        await context.Loans.AddAsync(loan, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken) =>
        context.SaveChangesAsync(cancellationToken);
}
