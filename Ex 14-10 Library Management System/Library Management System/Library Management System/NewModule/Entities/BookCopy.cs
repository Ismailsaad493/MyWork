namespace Library_Management_System.NewModule.Entities
{
    public class BookCopy : AuditableEntity
    {
        public int BookCopyId { get; set; }
        public int BookId { get; set; }
        public BookMaster? Book { get; set; } = null!;

        public int Status { get; set; } = 1; // 1 = Available, 2 = Issued
    }
}