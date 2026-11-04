using Library_Management_System.Entities;
using System;

namespace Library_Management_System.NewModule.Entities
{
    public class PaymentMaster : AuditableEntity
    {
        public string PaymentId { get; set; } = null!; // Auto-generates P1, P2...

        // Foreign Key to StudentMaster
        public string SId { get; set; } = null!;
        public StudentMaster Student { get; set; } = null!;

        public DateOnly PaymentDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public int PaymentMethod { get; set; } = 1; // "cash", "upi", or "banking"
        public decimal TotalAmount { get; set; }
    }
}