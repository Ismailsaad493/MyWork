namespace Book_Library_API.Entities
{
    public class Borrower
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public ICollection<BookLoan> CheckoutTransactions { get; set; } = new List<BookLoan>();
    }
}
