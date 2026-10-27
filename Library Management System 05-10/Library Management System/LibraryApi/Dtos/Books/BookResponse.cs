namespace LibraryApi.Dtos.Books;

public sealed record BookResponse(int Id, string Title, string Author, bool IsAvailable);
