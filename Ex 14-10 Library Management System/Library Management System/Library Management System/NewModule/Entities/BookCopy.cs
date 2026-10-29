using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.NewModule.Entities
{
    public class BookCopy : AuditableEntity
    {
        public int BookCopyId { get; set; }

        public int BookId { get; set; }
        public BookMaster? Book { get; set; }

        [RegularExpression("Available|Issued", ErrorMessage = "Status must be 'Available' or 'Issued'.")]
        public string Status { get; set; } = "Available";
    }
}