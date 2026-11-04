using Library_Management_System.Entities;
using System;
using System.Collections.Generic;

namespace Library_Management_System.NewModule.Entities
{
    public class IssueMaster : AuditableEntity
    {
        public string IssueId { get; set; } = null!; // i1, i2...

        public string SId { get; set; } = null!; // From Student Table

        public DateOnly IssueDate { get; set; }
        public int Status { get; set; } = 1; // "open" or "close"

        public ICollection<IssueDetailMaster> Details { get; set; } = new List<IssueDetailMaster>();
    }
}