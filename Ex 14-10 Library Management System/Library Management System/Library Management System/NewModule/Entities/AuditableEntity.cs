using Library_Management_System.Entities;
using System;

namespace Library_Management_System.NewModule.Entities
{
    public abstract class AuditableEntity : IAuditable
    {
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int UpdateCount { get; set; }
        public bool IsActive { get; set; } = true;
    }
}