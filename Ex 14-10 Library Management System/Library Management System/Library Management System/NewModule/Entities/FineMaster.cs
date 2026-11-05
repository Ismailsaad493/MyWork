using Library_Management_System.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.NewModule.Entities
{
    public class FineMaster : AuditableEntity
    {
        [Key] // Added this to explicitly tell EF Core this string is the Primary Key
        public string FineId { get; set; } = null!; // Auto-generates F1, F2...

        // Foreign Key to IssueDetailMaster
        public int IssueDetailId { get; set; }
        public IssueDetailMaster IssueDetail { get; set; } = null!;

        public DateOnly FineDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        public decimal FineAmount { get; set; }

        public int Status { get; set; } = 1; // "paid" or "unpaid"
    }
}