using System;
namespace Library_Management_System.NewModule.Dtos
{
    public class IssueCreateDto
    {
        public string SId { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "open";
    }

    public class IssueUpdateDto
    {
        public string SId { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}