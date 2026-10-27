namespace LibraryApi.Infrastructure.Entity;

public sealed class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
    public bool IsAvailable { get; set; } = true;
    public byte[] RowVersion { get; set; } = [];
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
