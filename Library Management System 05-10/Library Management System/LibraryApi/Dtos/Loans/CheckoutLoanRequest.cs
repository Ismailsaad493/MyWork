using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Dtos.Loans;

public sealed class CheckoutLoanRequest
{
    [Range(1, int.MaxValue)]
    public int BookId { get; init; }

    [Range(1, int.MaxValue)]
    public int MemberId { get; init; }

    [Range(1, 60)]
    public int? LoanPeriodDays { get; init; }
}
