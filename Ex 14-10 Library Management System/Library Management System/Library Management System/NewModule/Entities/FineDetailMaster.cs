using Library_Management_System.Entities;

namespace Library_Management_System.NewModule.Entities
{
    public class FineDetailMaster : AuditableEntity
    {
        public string FineDetailId { get; set; } = null!; // Auto-generates FD1, FD2...

        // Foreign Key to FineMaster
        public string FineId { get; set; } = null!;
        public FineMaster Fine { get; set; } = null!;

        public string ChargeType { get; set; } = null!; // "over due" or "damage"
        public decimal Amount { get; set; }
    }
}