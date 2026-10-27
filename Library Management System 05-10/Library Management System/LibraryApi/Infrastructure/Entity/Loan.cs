namespace LibraryApi.Infrastructure.Entity;

public sealed class Loan
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int MemberId { get; set; }
    public DateTime BorrowedAtUtc { get; set; }
    public DateTime DueAtUtc { get; set; }
    public DateTime? ReturnedAtUtc { get; set; }
    public Book Book { get; set; } = null!;
    public Member Member { get; set; } = null!;
}
