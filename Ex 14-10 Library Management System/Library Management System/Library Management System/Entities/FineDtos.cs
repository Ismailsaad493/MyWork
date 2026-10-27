namespace Library_Management_System.Entities
{
    public class CreateFineDto
    {
        public int StudentId { get; set; }
        public int BookId { get; set; }
        public string FineType { get; set; } = string.Empty; // "Book Damage" or "Late Submission"
        public decimal? Amount { get; set; } // Nullable because Book Damage ignores user input
    }

    public class FineResponseDto
    {
        public int FineId { get; set; }
        public int StudentId { get; set; }
        public int? BookId { get; set; }
        public decimal Amount { get; set; }
        public string FineType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime? PaymentDate { get; set; }
    }
}