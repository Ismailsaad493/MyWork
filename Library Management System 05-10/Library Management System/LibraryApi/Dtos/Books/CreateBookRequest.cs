using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Dtos.Books;

public sealed class CreateBookRequest
{
    [Required, StringLength(200, MinimumLength = 1)]
    public string Title { get; init; } = string.Empty;

    [Required, StringLength(150, MinimumLength = 1)]
    public string Author { get; init; } = string.Empty;
}
