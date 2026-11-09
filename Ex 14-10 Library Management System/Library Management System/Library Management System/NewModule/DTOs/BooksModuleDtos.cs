using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library_Management_System.NewModule.Dtos
{
    // --- Category DTOs ---
    public class CategoryCreateDto
    {
        // FIXED: Changed to "cName" to match Postman exactly
        [JsonPropertyName("cName")]
        public string CName { get; set; } = string.Empty;
    }

    public class CategoryResponseDto
    {
        [JsonPropertyName("cId")]
        public int CId { get; set; }

        [JsonPropertyName("cName")]
        public string CName { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }

    public class CategoryUpdateDto
    {
        public string CName { get; set; } = string.Empty;
    }

    // --- Publisher DTOs ---
    public record PublisherCreateDto(string PName);
    public record PublisherResponseDto(int PId, string PName);

    public class PublisherUpdateDto
    {
        public string PName { get; set; } = string.Empty;
    }

    // --- Master DTOs ---
    public class BookMasterCreateDto
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("isbn")]
        public string? Isbn { get; set; }

        [JsonPropertyName("cId")]
        public int CId { get; set; }

        [JsonPropertyName("pId")]
        public int PId { get; set; }

        // NEW: This allows Postman to accept a list of copies
        [JsonPropertyName("copies")]
        public List<BookCopyCreateDto> Copies { get; set; } = new();
    }

    public class BookMasterUpdateDto
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

        public int CId { get; set; }
        public int PId { get; set; }

        public string? Isbn { get; set; }

        public List<BookCopyDetailDto> Copies { get; set; } = new();
    }

    // --- Detail DTOs ---
    public class BookCopyDetailDto
    {
        public int BookCopyId { get; set; }
        public int BookId { get; set; }
        public int Status { get; set; }
    }

    public class BookCopyCreateDto
    {
        [JsonPropertyName("bookId")]
        public int BookId { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; } = 1;
    }

    public class BookCopyUpdateDto
    {
        public int Status { get; set; }
    }

    public class BookCopyResponseDto
    {
        public int BookCopyId { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public int Status { get; set; }
    }
}