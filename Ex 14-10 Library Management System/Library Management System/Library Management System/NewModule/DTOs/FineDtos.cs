using System;
using System.Collections.Generic;

namespace Library_Management_System.NewModule.Dtos
{
    // --- THIS WAS MISSING: Required for your GET request to work ---
    public class FineResponseDto
    {
        public string FineId { get; set; } = string.Empty; // Ensures the string maps correctly
        public int IssueDetailId { get; set; }
        public DateOnly FineDate { get; set; }
        public decimal FineAmount { get; set; }
        public int Status { get; set; }
    }

    // --- Create and Update DTOs ---
    public class FineCreateDto
    {
        public int IssueDetailId { get; set; }
        public decimal FineAmount { get; set; }
        public int Status { get; set; } = 1;
    }

    public class FineUpdateDto
    {
        public int IssueDetailId { get; set; }
        public decimal FineAmount { get; set; }
        public int Status { get; set; } = 1;
    }

    // --- Detail DTOs ---
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
        public int FineDetailId { get; set; }
        public string FineId { get; set; } = string.Empty;
        public int ChargeType { get; set; }
        public decimal Amount { get; set; }
    }
}