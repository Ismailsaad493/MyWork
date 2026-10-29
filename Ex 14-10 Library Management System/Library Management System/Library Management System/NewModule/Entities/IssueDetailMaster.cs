using Library_Management_System.Entities;
using System;

namespace Library_Management_System.NewModule.Entities
{
    public class IssueDetailMaster : AuditableEntity
    {
        public int IssueDetailId { get; set; } // 3001, 3002, 3003...

        // Foreign Key to IssueMaster
        public string IssueId { get; set; } = null!;
        public IssueMaster Issue { get; set; } = null!;

        // Foreign Key to BookCopy
        public int BookCopyId { get; set; }
        public BookCopy BookCopy { get; set; } = null!;

        public DateTime DueDate { get; set; }
        public string Status { get; set; } = null!; // "issued" or "available"
    }
}