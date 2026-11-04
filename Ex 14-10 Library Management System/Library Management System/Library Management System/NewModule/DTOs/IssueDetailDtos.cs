using System;

namespace Library_Management_System.NewModule.Dtos
{
    public class IssueDetailCreateDto
    {
        public string IssueId { get; set; } = string.Empty;
        public int BookCopyId { get; set; }
        public DateOnly DueDate { get; set; }
        public string Status { get; set; } = "issued";
    }

    public class IssueDetailUpdateDto
    {
        public string IssueId { get; set; } = string.Empty;
        public int BookCopyId { get; set; }
        public DateOnly DueDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class IssueDetailResponseDto
    {
        public int IssueDetailId { get; set; }
        public string IssueId { get; set; } = string.Empty;
        public int BookCopyId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public DateOnly DueDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}