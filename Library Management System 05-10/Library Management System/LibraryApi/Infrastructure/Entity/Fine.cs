using LibraryApi.Infrastructure.Models;

namespace LibraryApi.Infrastructure.Entity;

public sealed class Fine
{
    public int Id { get; set; }
    public int BookLoanId { get; set; }
    public FineType FineType { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateOnly? PaymentDate { get; set; }
    public string? PaymentMethod { get; set; }
}
