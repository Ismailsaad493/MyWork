using Library_Management_System.Entities;

namespace Library_Management_System.NewModule.Entities
{
    public class BookAvailabilityMaster : AuditableEntity
    {
        public string AvailabilityId { get; set; } = null!; // Auto-generates BA1, BA2...

        public int BookId { get; set; }
        public BookMaster Book { get; set; } = null!;

        public string Title { get; set; } = null!;
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
        public int IssuedCopies { get; set; }
    }
}