namespace LibraryApi.Infrastructure.Entity;

public sealed class Member
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
