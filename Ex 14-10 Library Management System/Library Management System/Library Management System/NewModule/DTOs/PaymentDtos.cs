using System;

namespace Library_Management_System.NewModule.Dtos
{
    public class PaymentCreateDto
    {
        public string SId { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!;
    }

    public class PaymentUpdateDto
    {
        public string SId { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!;
    }

    public class PaymentDetailCreateDto
    {
        public string PaymentId { get; set; } = null!;
        public string FineId { get; set; } = null!;
    }

    public class PaymentDetailUpdateDto
    {
        public string PaymentId { get; set; } = null!;
        public string FineId { get; set; } = null!;
    }
}