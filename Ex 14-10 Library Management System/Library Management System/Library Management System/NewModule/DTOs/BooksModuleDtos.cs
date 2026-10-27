namespace Library_Management_System.NewModule.DTOs
{
    public class BooksModuleDtos
    {
    }
}
namespace Library_Management_System.NewModule.Dtos
{
    // Category DTOs
    public record CategoryCreateDto(string CName);
    public record CategoryResponseDto(int CId, string CName);

    // Publisher DTOs
    public record PublisherCreateDto(string PName);
    public record PublisherResponseDto(int PId, string PName);

    // BookMaster DTOs
    public class BookMasterCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Isbn { get; set; }
        public int CId { get; set; }
        public int PId { get; set; }
    }

    public class BookMasterResponseDto
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Isbn { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string PublisherName { get; set; } = string.Empty;
    }

    // BookCopy DTOs
    public class BookCopyCreateDto
    {
        public int BookId { get; set; }
        public string Status { get; set; } = "Available";
    }

    public class BookCopyResponseDto
    {
        public int BookCopyId { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}