using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Dtos.Fines;

public sealed class CreateFineRequest
{
    [Required, StringLength(30)]
    public string FineType { get; init; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int BookId { get; init; }

    [Range(typeof(decimal), "0.01", "1000000.00")]
    public decimal? Amount { get; init; }
}
