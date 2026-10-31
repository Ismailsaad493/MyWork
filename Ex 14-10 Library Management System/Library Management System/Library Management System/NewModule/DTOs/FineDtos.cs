using System;

namespace Library_Management_System.NewModule.Dtos
{
    public class FineCreateDto
    {
        public int IssueDetailId { get; set; }
        public decimal FineAmount { get; set; }
        public string Status { get; set; } = null!;
    }

    public class FineUpdateDto
    {
        public int IssueDetailId { get; set; }
        public decimal FineAmount { get; set; }
        public string Status { get; set; } = null!;
    }

    public class FineDetailCreateDto
    {
        public string FineId { get; set; } = null!;
        public string ChargeType { get; set; } = null!;
    }

    public class FineDetailUpdateDto
    {
        public string FineId { get; set; } = null!;
        public string ChargeType { get; set; } = null!;
    }
}