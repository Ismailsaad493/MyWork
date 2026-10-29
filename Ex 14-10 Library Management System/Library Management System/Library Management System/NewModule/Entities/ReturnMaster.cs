using Library_Management_System.Entities;
using System;

namespace Library_Management_System.NewModule.Entities
{
    public class ReturnMaster : AuditableEntity
    {
        public string RId { get; set; } = null!; // Auto-generates R1, R2...

        // Foreign Key to StudentMaster
        public string SId { get; set; } = null!;

        public DateTime ReturnDate { get; set; } = DateTime.UtcNow;
    }
}