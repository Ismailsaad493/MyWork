using System.Collections.Generic;
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
    // --- Master DTOs ---
    public class BookMasterCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Isbn { get; set; }
        public int CId { get; set; }
        public int PId { get; set; }
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

        // Replaced CategoryName and PublisherName with IDs to match your format
        public int CId { get; set; }
        public int PId { get; set; }

        public string? Isbn { get; set; }

        public List<BookCopyDetailDto> Copies { get; set; } = new();
    }

    // --- Detail DTOs ---
    public class BookCopyDetailDto
    {
        public int BookCopyId { get; set; }
        public int BookId { get; set; } // Added to match your requested format
        public int Status { get; set; }
    }

    public class BookCopyCreateDto
    {
        public int BookId { get; set; }
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