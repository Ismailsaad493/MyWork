namespace LibraryApi.Infrastructure.Models;

public sealed class FineRecord
{
    public int Id { get; set; }
    public int BookLoanId { get; set; }
    public string FineType { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateOnly? PaymentDate { get; set; }
    public string? PaymentMethod { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public int BorrowerId { get; set; }
    public string BorrowerName { get; set; } = string.Empty;
}
