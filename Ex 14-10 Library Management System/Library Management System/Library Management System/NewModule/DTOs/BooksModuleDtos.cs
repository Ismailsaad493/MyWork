using System.Text.Json.Serialization;

namespace Library_Management_System.NewModule.Dtos
{
    // --- Category DTOs ---
    public class CategoryCreateDto
    {
        [JsonPropertyName("C_name")]
        public string CName { get; set; } = string.Empty;
    }

    public class CategoryResponseDto
    {
        [JsonPropertyName("C_id")]
        public int CId { get; set; }

        [JsonPropertyName("C_name")]
        public string CName { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }

    // --- Publisher DTOs ---
    public record PublisherCreateDto(string PName);
    public record PublisherResponseDto(int PId, string PName);

    // --- BookMaster DTOs ---
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

    // --- BookCopy DTOs ---
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
    // --- Update DTOs ---
    public class CategoryUpdateDto
    {
        public string CName { get; set; } = string.Empty;
    }

    public class PublisherUpdateDto
    {
        public string PName { get; set; } = string.Empty;
    }

    public class BookMasterUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Isbn { get; set; }
        public int CId { get; set; }
        public int PId { get; set; }
    }

    public class BookCopyUpdateDto
    {
        public string Status { get; set; } = string.Empty;
    }
}