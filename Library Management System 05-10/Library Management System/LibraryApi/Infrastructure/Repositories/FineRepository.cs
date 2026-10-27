using LibraryApi.Infrastructure.Context;
using LibraryApi.Infrastructure.Entity;
using LibraryApi.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Infrastructure.Repositories;

public sealed class FineRepository(LibraryDbContext context) : IFineRepository
{
    public async Task<IReadOnlyList<FineRecord>> GetAllAsync(CancellationToken cancellationToken) =>
        await FineRecords()
            .OrderByDescending(fine => fine.CreatedAtUtc)
            .ThenByDescending(fine => fine.Id)
            .ToListAsync(cancellationToken);

    public Task<FineRecord?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        FineRecords().SingleOrDefaultAsync(fine => fine.Id == id, cancellationToken);

    public Task<Fine?> GetTrackedByIdAsync(int id, CancellationToken cancellationToken) =>
        context.Fines.SingleOrDefaultAsync(fine => fine.Id == id, cancellationToken);

    public Task<FineLoanReference?> GetLatestLoanForBookAsync(
        int bookId,
        CancellationToken cancellationToken) =>
        context.Database.SqlQuery<FineLoanReference>($"""
            SELECT TOP (1)
                bl.Id AS BookLoanId,
                bl.BookId,
                b.Title AS BookTitle,
                bl.BorrowerId,
                borrower.Name AS BorrowerName,
                bl.DueDate,
                bl.ReturnDate
            FROM dbo.BookLoans AS bl
            INNER JOIN dbo.Books AS b ON b.Id = bl.BookId
            INNER JOIN dbo.Borrowers AS borrower ON borrower.Id = bl.BorrowerId
            WHERE bl.BookId = {bookId}
            ORDER BY bl.BorrowDate DESC, bl.Id DESC
            """).FirstOrDefaultAsync(cancellationToken);

    public Task<bool> ExistsForLoanAsync(
        int bookLoanId,
        FineType fineType,
        CancellationToken cancellationToken) =>
        context.Fines.AnyAsync(
            fine => fine.BookLoanId == bookLoanId && fine.FineType == fineType,
            cancellationToken);

    public async Task AddAsync(Fine fine, CancellationToken cancellationToken)
    {
        await context.Fines.AddAsync(fine, cancellationToken);
        await SaveChangesAsync(cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken) =>
        context.SaveChangesAsync(cancellationToken);

    private IQueryable<FineRecord> FineRecords() => context.Database.SqlQuery<FineRecord>($"""
        SELECT
            fine.Id,
            fine.BookLoanId,
            fine.FineType,
            fine.Amount,
            fine.CreatedAtUtc,
            fine.PaymentDate,
            fine.PaymentMethod,
            loan.BookId,
            book.Title AS BookTitle,
            loan.BorrowerId,
            borrower.Name AS BorrowerName
        FROM dbo.Fines AS fine
        INNER JOIN dbo.BookLoans AS loan ON loan.Id = fine.BookLoanId
        INNER JOIN dbo.Books AS book ON book.Id = loan.BookId
        INNER JOIN dbo.Borrowers AS borrower ON borrower.Id = loan.BorrowerId
        """);
}
