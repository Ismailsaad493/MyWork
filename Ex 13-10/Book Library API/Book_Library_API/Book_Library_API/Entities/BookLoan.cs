namespace Book_Library_API.Entities
{
    public class BookLoan
    {
        public int Id { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public int BookId { get; set; }
        public Book? Book { get; set; }

        public int BorrowerId { get; set; }
        public Borrower? Borrower { get; set; }
    }
}