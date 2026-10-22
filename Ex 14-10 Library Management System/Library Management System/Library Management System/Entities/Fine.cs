namespace Library_Management_System.Entities
{
    public class Fine : IAuditable
    {
        public int Id { get; set; }

        public int BookLoanId { get; set; }
        public BookLoan? BookLoan { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public decimal Amount { get; set; }
        public bool IsPaid { get; set; } = false;
        public DateTime? PaymentDate { get; set; }

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public int UpdateCount { get; set; } = 0;
    }
}