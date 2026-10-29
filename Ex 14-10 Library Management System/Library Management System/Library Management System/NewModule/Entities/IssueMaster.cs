using Library_Management_System.Entities;
using System;
using System.Collections.Generic;

namespace Library_Management_System.NewModule.Entities
{
    public class IssueMaster : AuditableEntity
    {
        public string IssueId { get; set; } = null!; // i1, i2...

        public string SId { get; set; } = null!; // From Student Table

        public DateTime IssueDate { get; set; }
        public string Status { get; set; } = null!; // "open" or "close"

        // Navigation property for 1-to-Many relationship
    }
}