using Library_Management_System.Entities;
using System;

namespace Library_Management_System.NewModule.Entities
{
    public class FineMaster : AuditableEntity
    {
        public string FineId { get; set; } = null!; // Auto-generates F1, F2...

        // Foreign Key to IssueDetailMaster
        public int IssueDetailId { get; set; }
        public IssueDetailMaster IssueDetail { get; set; } = null!;

        public DateOnly FineDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow); public decimal FineAmount { get; set; }

        public string Status { get; set; } = null!; // "paid" or "unpaid"
    }
}