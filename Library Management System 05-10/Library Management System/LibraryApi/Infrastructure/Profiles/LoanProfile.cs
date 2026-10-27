using LibraryApi.Dtos.Loans;
using LibraryApi.Infrastructure.Entity;

namespace LibraryApi.Infrastructure.Profiles;

public sealed class LoanProfile
{
    public LoanResponse ToResponse(Loan loan) => new(
        loan.Id,
        loan.BookId,
        loan.Book.Title,
        loan.MemberId,
        loan.Member.FullName,
        loan.BorrowedAtUtc,
        loan.DueAtUtc,
        loan.ReturnedAtUtc);
}
