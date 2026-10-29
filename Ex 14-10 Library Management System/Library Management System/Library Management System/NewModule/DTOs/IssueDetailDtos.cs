using System;

namespace Library_Management_System.NewModule.Dtos
{
    public class IssueDetailCreateDto
    {
        public string IssueId { get; set; } = string.Empty;
        public int BookCopyId { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = "issued";
    }

    public class IssueDetailUpdateDto
    {
        public string IssueId { get; set; } = string.Empty;
        public int BookCopyId { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}