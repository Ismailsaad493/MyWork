using LibraryApi.Dtos.Books;
using LibraryApi.Infrastructure.Entity;

namespace LibraryApi.Infrastructure.Profiles;

public sealed class BookProfile
{
    public Book ToEntity(CreateBookRequest request) => new()
    {
        Title = request.Title.Trim(),
        Author = request.Author.Trim()
    };

    public BookResponse ToResponse(Book book) =>
        new(book.Id, book.Title, book.Author, book.IsAvailable);

    public void Apply(UpdateBookRequest request, Book book)
    {
        book.Title = request.Title.Trim();
        book.Author = request.Author.Trim();
    }
}
