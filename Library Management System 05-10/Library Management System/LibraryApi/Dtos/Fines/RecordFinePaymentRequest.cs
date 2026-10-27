using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Dtos.Fines;

public sealed class RecordFinePaymentRequest
{
    [Required]
    public DateOnly? PaymentDate { get; init; }

    [Required, StringLength(10)]
    public string PaymentMethod { get; init; } = string.Empty;
}
