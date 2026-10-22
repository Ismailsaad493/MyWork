namespace Library_Management_System.Entities
{
    public class Book : IAuditable
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public int Quantity { get; set; }

        public string Category { get; set; } = string.Empty;

        public bool IsAvailable { get; set; } = true;

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public int UpdateCount { get; set; } = 0;
    }
}