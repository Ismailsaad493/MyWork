namespace Library_Management_System.Dtos
{
    public class BookCreateDto
    {
        public string Title { get; set; } = null!;
        public int AuthorId { get; set; }
        public string Isbn { get; set; } = null!;
        public string Publisher { get; set; } = null!;
        public int Quantity { get; set; }
        public string Category { get; set; } = null!;
        public bool IsAvailable { get; set; }
    }
}