using LibraryApi.Infrastructure.Context;
using LibraryApi.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Infrastructure.Repositories;

public sealed class BookRepository(LibraryDbContext context) : IBookRepository
{
    public async Task<IReadOnlyList<Book>> GetAllAsync(string? author, CancellationToken cancellationToken)
    {
        IQueryable<Book> query = context.Books.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(author))
        {
            query = query.Where(book => book.Author.Contains(author.Trim()));
        }

        return await query.OrderBy(book => book.Title).ThenBy(book => book.Id)
            .ToListAsync(cancellationToken);
    }

    public Task<Book?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        context.Books.SingleOrDefaultAsync(book => book.Id == id, cancellationToken);

    public Task<bool> HasLoansAsync(int bookId, CancellationToken cancellationToken) =>
        context.Loans.AnyAsync(loan => loan.BookId == bookId, cancellationToken);

    public async Task AddAsync(Book book, CancellationToken cancellationToken)
    {
        await context.Books.AddAsync(book, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken) =>
        context.SaveChangesAsync(cancellationToken);

    public async Task DeleteAsync(Book book, CancellationToken cancellationToken)
    {
        context.Books.Remove(book);
        await context.SaveChangesAsync(cancellationToken);
    }
}
