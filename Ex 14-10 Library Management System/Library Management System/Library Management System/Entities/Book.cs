using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management_System.Entities
{
    public class Book 
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        // FIXED: Replaced string Author with a relational Foreign Key to the Authors table
        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public Author? Author { get; set; }

        public string ISBN { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;

        // Preserved your existing stock property
        public int Quantity { get; set; }

        public string Category { get; set; } = string.Empty;

        // Preserved your existing availability flag
        public bool IsAvailable { get; set; } = true;

        // Preserved all IAuditable properties
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public int UpdateCount { get; set; } = 0;

        // NEW: Navigation property to track multiple borrow records for this book
        public ICollection<BookLoan>? BookLoans { get; set; }
    }
}