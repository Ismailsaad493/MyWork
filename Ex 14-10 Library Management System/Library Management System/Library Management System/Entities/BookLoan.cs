namespace Library_Management_System.Entities
{
    public class BookLoan 
    {
        public int Id { get; set; }

        // EF Core will automatically link these two based on the naming convention
        public int BookId { get; set; }
        public Book? Book { get; set; }

        // EF Core will automatically link these two based on the naming convention
        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public DateTime LoanDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; } = false;

        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public int UpdateCount { get; set; } = 0;
    }
}