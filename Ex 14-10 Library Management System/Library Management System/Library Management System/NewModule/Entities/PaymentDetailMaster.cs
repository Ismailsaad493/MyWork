using Library_Management_System.Entities;

namespace Library_Management_System.NewModule.Entities
{
    public class PaymentDetailMaster : AuditableEntity
    {
        public string PaymentDetailId { get; set; } = null!; // Auto-generates PD1, PD2...

        // Foreign Key to PaymentMaster
        public string PaymentId { get; set; } = null!;
        public PaymentMaster Payment { get; set; } = null!;

        // Foreign Key to FineMaster
        public string FineId { get; set; } = null!;
        public FineMaster Fine { get; set; } = null!;

        public decimal PaidAmount { get; set; }
    }
}