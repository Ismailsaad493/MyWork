namespace LibraryApi.Infrastructure.Models;

public sealed class FineLoanReference
{
    public int BookLoanId { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public int BorrowerId { get; set; }
    public string BorrowerName { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}
