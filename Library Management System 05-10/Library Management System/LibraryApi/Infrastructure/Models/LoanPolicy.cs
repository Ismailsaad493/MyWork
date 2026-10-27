namespace LibraryApi.Infrastructure.Models;

public sealed class LoanPolicy
{
    public const string SectionName = "LoanPolicy";

    public int DefaultLoanPeriodDays { get; set; } = 14;
    public int MaximumLoanPeriodDays { get; set; } = 60;
}
