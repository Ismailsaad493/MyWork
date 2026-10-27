using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management_System.Entities
{
    public class Fine
    {
        public int Id { get; set; }

        public int BookLoanId { get; set; }

        [ForeignKey("BookLoanId")]
        public BookLoan? BookLoan { get; set; }

        // FIXED: Changed UserId to StudentId so it links to the new Student table
        public int StudentId { get; set; }
        public DateTime? PaymentDate { get; set; }

        [ForeignKey("StudentId")]
        public Student? Student { get; set; }

        public decimal Amount { get; set; }
        public bool IsPaid { get; set; } = false;
    }
}