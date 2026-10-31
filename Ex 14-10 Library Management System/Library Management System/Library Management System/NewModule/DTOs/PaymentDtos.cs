using System;

namespace Library_Management_System.NewModule.Dtos
{
    public class PaymentCreateDto
    {
        public string SId { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!; // "cash", "upi", or "banking"
        public decimal TotalAmount { get; set; }
    }

    public class PaymentUpdateDto
    {
        public string SId { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!;
        public decimal TotalAmount { get; set; }
    }

    public class PaymentDetailCreateDto
    {
        public string PaymentId { get; set; } = null!;
        public string FineId { get; set; } = null!;
        public decimal PaidAmount { get; set; }
    }

    public class PaymentDetailUpdateDto
    {
        public string PaymentId { get; set; } = null!;
        public string FineId { get; set; } = null!;
        public decimal PaidAmount { get; set; }
    }
}