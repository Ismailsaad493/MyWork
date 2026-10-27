using System.ComponentModel.DataAnnotations;
using LibraryApi.Dtos.Books;
using LibraryApi.Infrastructure.Profiles;
using LibraryApi.Infrastructure.Repositories;
using LibraryApi.Infrastructure.Services.Interfaces;

namespace LibraryApi.Infrastructure.Services;

public sealed class BookService(IBookRepository books, BookProfile profile) : IBookService
{
    public async Task<IReadOnlyList<BookResponse>> GetAllAsync(string? author, CancellationToken cancellationToken) =>
        (await books.GetAllAsync(author, cancellationToken)).Select(profile.ToResponse).ToArray();

    public async Task<BookResponse> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        profile.ToResponse(await GetRequiredBookAsync(id, cancellationToken));

    public async Task<BookResponse> CreateAsync(CreateBookRequest request, CancellationToken cancellationToken)
    {
        EnsureBookFields(request.Title, request.Author);
        var book = profile.ToEntity(request);
        await books.AddAsync(book, cancellationToken);
        return profile.ToResponse(book);
    }

    public async Task<BookResponse> UpdateAsync(
        int id,
        UpdateBookRequest request,
        CancellationToken cancellationToken)
    {
        EnsureBookFields(request.Title, request.Author);
        var book = await GetRequiredBookAsync(id, cancellationToken);
        profile.Apply(request, book);
        await books.SaveChangesAsync(cancellationToken);
        return profile.ToResponse(book);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var book = await GetRequiredBookAsync(id, cancellationToken);
        if (await books.HasLoansAsync(id, cancellationToken))
        {
            throw new ConflictException("A book with loan history cannot be deleted.");
        }

        await books.DeleteAsync(book, cancellationToken);
    }

    private async Task<Infrastructure.Entity.Book> GetRequiredBookAsync(int id, CancellationToken cancellationToken) =>
        await books.GetByIdAsync(id, cancellationToken)
        ?? throw new EntityNotFoundException($"Book {id} was not found.");

    private static void EnsureBookFields(string title, string author)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ValidationException("Title is required.");
        }

        if (string.IsNullOrWhiteSpace(author))
        {
            throw new ValidationException("Author is required.");
        }
    }
}
