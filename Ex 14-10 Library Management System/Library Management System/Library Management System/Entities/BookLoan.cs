using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management_System.Entities
{
    public class BookLoan : IAuditable
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }

        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
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