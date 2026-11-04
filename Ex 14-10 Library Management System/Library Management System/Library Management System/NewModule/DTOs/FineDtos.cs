using System;

namespace Library_Management_System.NewModule.Dtos
{
    public class FineCreateDto
    {
        public int IssueDetailId { get; set; }
        public decimal FineAmount { get; set; }
        public int Status { get; set; } =1;
    }

    public class FineUpdateDto
    {
        public int IssueDetailId { get; set; }
        public decimal FineAmount { get; set; }
        public int Status { get; set; } = 1;
    }

    public class FineDetailCreateDto
    {
        public string FineId { get; set; } = null!;
        public int ChargeType { get; set; } = 1;
    }

    public class FineDetailUpdateDto
    {
        public string FineId { get; set; } = null!;
        public int ChargeType { get; set; } = 1;
    }
    public class FineDetailResponseDto
    {
        public int FineDetailId { get; set; } // Must match this exact spelling and casing
        public string FineId { get; set; } = string.Empty;
        public int ChargeType { get; set; }
        public decimal Amount { get; set; }
    }
}