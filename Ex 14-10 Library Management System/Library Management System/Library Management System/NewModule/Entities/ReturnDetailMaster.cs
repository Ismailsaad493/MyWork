using Library_Management_System.Entities;
using System;

namespace Library_Management_System.NewModule.Entities
{
    public class ReturnDetailMaster : AuditableEntity
    {
        public string ReturnDetailId { get; set; } = null!; // Auto-generates RD1, RD2...

        // Foreign Key to ReturnMaster
        public string RId { get; set; } = null!;
        public ReturnMaster Return { get; set; } = null!;

        // Foreign Key to IssueDetailMaster
        public int IssueDetailId { get; set; }
        public IssueDetailMaster IssueDetail { get; set; } = null!;

        public string Condition { get; set; } = null!; // "good", "bad", or "ugly"
    }
}