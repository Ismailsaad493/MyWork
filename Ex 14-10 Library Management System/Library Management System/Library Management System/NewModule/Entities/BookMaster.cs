namespace Library_Management_System.NewModule.Entities
{
    public class BookMaster
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Isbn { get; set; }

        public int CId { get; set; }
        public CategoryMaster? Category { get; set; }

        public int PId { get; set; }
        public PublisherMaster? Publisher { get; set; }

        // Navigation property
        public ICollection<BookCopy>? Copies { get; set; }
    }
}