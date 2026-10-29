using System;

namespace Library_Management_System.NewModule.Dtos
{
    public class ReturnCreateDto
    {
        public string SId { get; set; } = null!;
    }

    public class ReturnUpdateDto
    {
        public string SId { get; set; } = null!;
    }

    public class ReturnDetailCreateDto
    {
        public string RId { get; set; } = null!;
        public int IssueDetailId { get; set; }
        public string Condition { get; set; } = null!; // "good", "bad", or "ugly"
    }

    public class ReturnDetailUpdateDto
    {
        public string RId { get; set; } = null!;
        public int IssueDetailId { get; set; }
        public string Condition { get; set; } = null!;
    }
}