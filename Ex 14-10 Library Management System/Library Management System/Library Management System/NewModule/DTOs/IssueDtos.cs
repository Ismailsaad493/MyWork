using System;
using System.Collections.Generic;

namespace Library_Management_System.NewModule.Dtos
{
    // --- Issue Detail DTOs ---
    public class IssueDetailCreateDto
    {
        public string IssueId { get; set; } = string.Empty; // Added
        public int BookCopyId { get; set; }
        public DateOnly DueDate { get; set; }
        public int Status { get; set; } = 1;
    }

    public class IssueDetailUpdateDto
    {
        public string IssueId { get; set; } = string.Empty; // Added
        public int BookCopyId { get; set; }
        public DateOnly DueDate { get; set; }
        public int Status { get; set; }
    }

    public class IssueDetailResponseDto
    {
        public int IssueDetailId { get; set; }
        public string IssueId { get; set; } = string.Empty;
        public int BookCopyId { get; set; }
        public string BookTitle { get; set; } = string.Empty; // Added
        public DateOnly DueDate { get; set; }
        public int Status { get; set; }
    }

    // --- Issue Master DTOs ---
    public class IssueCreateDto
    {
        public string SId { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        public int Status { get; set; } = 1;
        public List<IssueDetailCreateDto> Details { get; set; } = new();
    }

    public class IssueUpdateDto
    {
        public string SId { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public int Status { get; set; }
    }

    public class IssueResponseDto
    {
        public string IssueId { get; set; } = string.Empty;
        public string SId { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public int Status { get; set; }
        public List<IssueDetailResponseDto> Details { get; set; } = new();
    }
}