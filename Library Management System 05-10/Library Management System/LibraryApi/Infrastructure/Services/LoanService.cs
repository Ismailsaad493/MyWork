using System.ComponentModel.DataAnnotations;
using LibraryApi.Dtos.Loans;
using LibraryApi.Infrastructure.Entity;
using LibraryApi.Infrastructure.Models;
using LibraryApi.Infrastructure.Profiles;
using LibraryApi.Infrastructure.Repositories;
using LibraryApi.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace LibraryApi.Infrastructure.Services;

public sealed class LoanService(
    ILoanRepository loans,
    IBookRepository books,
    IMemberRepository members,
    LoanProfile profile,
    IOptions<LoanPolicy> loanPolicy) : ILoanService
{
    private readonly LoanPolicy _policy = loanPolicy.Value;

    public async Task<IReadOnlyList<LoanResponse>> GetAllAsync(CancellationToken cancellationToken) =>
        (await loans.GetAllAsync(cancellationToken)).Select(profile.ToResponse).ToArray();

    public async Task<LoanResponse> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var loan = await loans.GetByIdAsync(id, cancellationToken)
            ?? throw new EntityNotFoundException($"Loan {id} was not found.");
        return profile.ToResponse(loan);
    }

    public async Task<LoanResponse> CheckoutAsync(
        CheckoutLoanRequest request,
        CancellationToken cancellationToken)
    {
        var book = await books.GetByIdAsync(request.BookId, cancellationToken)
            ?? throw new EntityNotFoundException($"Book {request.BookId} was not found.");
        var member = await members.GetByIdAsync(request.MemberId, cancellationToken)
            ?? throw new EntityNotFoundException($"Member {request.MemberId} was not found.");

        if (!book.IsAvailable)
        {
            throw new ConflictException("This book is already checked out.");
        }

        var loanPeriodDays = request.LoanPeriodDays ?? _policy.DefaultLoanPeriodDays;
        if (loanPeriodDays > _policy.MaximumLoanPeriodDays)
        {
            throw new ValidationException($"Loan periods cannot exceed {_policy.MaximumLoanPeriodDays} days.");
        }

        var now = DateTime.UtcNow;
        var loan = new Loan
        {
            Book = book,
            BookId = book.Id,
            Member = member,
            MemberId = member.Id,
            BorrowedAtUtc = now,
            DueAtUtc = now.AddDays(loanPeriodDays)
        };

        book.IsAvailable = false;
        await loans.AddAsync(loan, cancellationToken);
        return profile.ToResponse(loan);
    }

    public async Task<LoanResponse> ReturnAsync(int id, CancellationToken cancellationToken)
    {
        var loan = await loans.GetActiveByIdAsync(id, cancellationToken);
        if (loan is null)
        {
            if (await loans.GetByIdAsync(id, cancellationToken) is null)
            {
                throw new EntityNotFoundException($"Loan {id} was not found.");
            }

            throw new ConflictException("This loan has already been returned.");
        }

        loan.ReturnedAtUtc = DateTime.UtcNow;
        loan.Book.IsAvailable = true;
        await loans.SaveChangesAsync(cancellationToken);
        return profile.ToResponse(loan);
    }
}
