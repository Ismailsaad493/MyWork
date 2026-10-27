using LibraryApi.Dtos.Fines;
using LibraryApi.Infrastructure.Entity;
using LibraryApi.Infrastructure.Models;

namespace LibraryApi.Infrastructure.Profiles;

public sealed class FineProfile
{
    public FineResponse ToResponse(Fine fine, FineLoanReference loan) => new(
        fine.Id,
        GetDisplayName(fine.FineType),
        fine.Amount,
        loan.BookLoanId,
        loan.BookId,
        loan.BookTitle,
        loan.BorrowerId,
        loan.BorrowerName,
        fine.CreatedAtUtc,
        fine.PaymentDate,
        fine.PaymentMethod);

    public FineResponse ToResponse(FineRecord fine) => new(
        fine.Id,
        GetDisplayName(Enum.Parse<FineType>(fine.FineType, ignoreCase: true)),
        fine.Amount,
        fine.BookLoanId,
        fine.BookId,
        fine.BookTitle,
        fine.BorrowerId,
        fine.BorrowerName,
        fine.CreatedAtUtc,
        fine.PaymentDate,
        fine.PaymentMethod);

    public static string GetDisplayName(FineType fineType) => fineType switch
    {
        FineType.BookDamage => "Book Damage",
        FineType.LateSubmission => "Late Submission",
        _ => throw new ArgumentOutOfRangeException(nameof(fineType), fineType, "Unknown fine type.")
    };
}
