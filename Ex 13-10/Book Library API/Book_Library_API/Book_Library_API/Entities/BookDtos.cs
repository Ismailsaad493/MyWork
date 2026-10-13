namespace Book_Library_API.Entities
{
    public class AddBookDto
    {
        public string Title { get; set; } = string.Empty;
        public int PublishedYear { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
    }

    public class UpdateBookDto
    {
        public string Title { get; set; } = string.Empty;
        public int PublishedYear { get; set; }
        public bool IsAvailable { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
    }

    public class BookResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int PublishedYear { get; set; }
        public bool IsAvailable { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
    }
}