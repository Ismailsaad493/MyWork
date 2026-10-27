using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management_System.Entities
{
    public class Fine
    {
        public int Id { get; set; }

        public int? BookLoanId { get; set; } // FIXED: Kept only the nullable version

        [ForeignKey("BookLoanId")]
        public BookLoan? BookLoan { get; set; }

        // FIXED: Changed UserId to StudentId so it links to the new Student table
        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student? Student { get; set; }

        public int? BookId { get; set; } // Added to fix the CS0117 missing definition error

        public string FineType { get; set; } = "Late Submission"; // "Book Damage" or "Late Submission"

        public decimal Amount { get; set; }
        public bool IsPaid { get; set; } = false;

        public DateTime? PaymentDate { get; set; } // Kept only one instance to fix the CS0102 duplicate error
    }
}