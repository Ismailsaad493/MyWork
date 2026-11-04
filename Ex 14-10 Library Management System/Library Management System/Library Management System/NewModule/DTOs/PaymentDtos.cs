using System;

namespace Library_Management_System.NewModule.Dtos
{
    public class PaymentCreateDto
    {
        public string SId { get; set; } = null!;
        public int PaymentMethod { get; set; } = 1;
    }

    public class PaymentUpdateDto
    {
        public string SId { get; set; } = null!;
        public int PaymentMethod { get; set; } = 1;
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