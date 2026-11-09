using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library_Management_System.NewModule.Dtos
{
    // --- Issue Detail DTOs ---
    public class IssueDetailCreateDto
    {
        public int BookCopyId { get; set; }
        public DateOnly DueDate { get; set; }
        public int Status { get; set; } = 1;
    }

    public class IssueDetailUpdateDto
    {
        public string IssueId { get; set; } = string.Empty;
        public int BookCopyId { get; set; }
        public DateOnly DueDate { get; set; }
        public int Status { get; set; }
    }

    public class IssueDetailResponseDto
    {
        public int IssueDetailId { get; set; }
        public string IssueId { get; set; } = string.Empty;
        public int BookCopyId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public DateOnly DueDate { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; } = string.Empty; // <-- NEW
    }

    // --- Issue Master DTOs ---
    public class IssueCreateDto
    {
        public int BookId { get; set; }

        [JsonPropertyName("SId")]
        public string SId { get; set; } = string.Empty; // Adding "= string.Empty;" fixes the CS8618 yellow warning

        [JsonPropertyName("issueDate")]
        public DateTime IssueDate { get; set; }

        // --- ADD THESE TO FIX THE RED ERRORS ---
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("details")]
        public List<IssueDetailCreateDto> Details { get; set; } = new();
    }
    public class IssueUpdateDto
    {
        public string SId { get; set; } = string.Empty;
        public DateOnly IssueDate { get; set; }
        public int Status { get; set; }
    }

    public class IssueResponseDto
    {
        public string IssueId { get; set; } = string.Empty;
        public string SId { get; set; } = string.Empty;
        public DateOnly IssueDate { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; } = string.Empty; // <-- NEW
        public List<IssueDetailResponseDto> Details { get; set; } = new();
    }
}