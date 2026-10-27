using System.ComponentModel.DataAnnotations;
using LibraryApi.Dtos.Fines;
using LibraryApi.Infrastructure.Entity;
using LibraryApi.Infrastructure.Models;
using LibraryApi.Infrastructure.Profiles;
using LibraryApi.Infrastructure.Repositories;
using LibraryApi.Infrastructure.Services.Interfaces;

namespace LibraryApi.Infrastructure.Services;

public sealed class FineService(
    IFineRepository fines,
    FineProfile profile) : IFineService
{
    public async Task<IReadOnlyList<FineResponse>> GetAllAsync(CancellationToken cancellationToken) =>
        (await fines.GetAllAsync(cancellationToken)).Select(profile.ToResponse).ToArray();

    public async Task<FineResponse> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var fine = await fines.GetByIdAsync(id, cancellationToken)
            ?? throw new EntityNotFoundException($"Fine {id} was not found.");
        return profile.ToResponse(fine);
    }

    public async Task<FineResponse> CreateAsync(
        CreateFineRequest request,
        CancellationToken cancellationToken)
    {
        var fineType = ParseFineType(request.FineType);
        var loan = await fines.GetLatestLoanForBookAsync(request.BookId, cancellationToken)
            ?? throw new EntityNotFoundException(
                $"No borrower loan was found for book {request.BookId}.");

        var amount = fineType switch
        {
            FineType.BookDamage => GetBookDamageAmount(request.Amount),
            FineType.LateSubmission => GetLateSubmissionAmount(request.Amount),
            _ => throw new ValidationException("The selected fine type is not supported.")
        };

        if (fineType == FineType.LateSubmission
            && (loan.ReturnDate is null || loan.ReturnDate <= loan.DueDate))
        {
            throw new ValidationException("A Late Submission fine requires a returned loan that was returned after its due date.");
        }

        if (await fines.ExistsForLoanAsync(loan.BookLoanId, fineType, cancellationToken))
        {
            throw new ConflictException($"A {FineProfile.GetDisplayName(fineType)} fine already exists for this loan.");
        }

        var fine = new Fine
        {
            BookLoanId = loan.BookLoanId,
            FineType = fineType,
            Amount = amount,
            CreatedAtUtc = DateTime.UtcNow
        };

        await fines.AddAsync(fine, cancellationToken);
        return profile.ToResponse(fine, loan);
    }

    public async Task<FineResponse> RecordPaymentAsync(
        int fineId,
        RecordFinePaymentRequest request,
        CancellationToken cancellationToken)
    {
        var fine = await fines.GetTrackedByIdAsync(fineId, cancellationToken)
            ?? throw new EntityNotFoundException($"Fine {fineId} was not found.");

        if (fine.Amount <= 0)
        {
            throw new ValidationException("A payment cannot be recorded for a zero-value fine.");
        }

        if (fine.PaymentDate is not null || fine.PaymentMethod is not null)
        {
            throw new ConflictException($"Fine {fineId} has already been paid.");
        }

        fine.PaymentDate = request.PaymentDate
            ?? throw new ValidationException("PaymentDate is required.");
        fine.PaymentMethod = ParsePaymentMethod(request.PaymentMethod);

        await fines.SaveChangesAsync(cancellationToken);

        var record = await fines.GetByIdAsync(fineId, cancellationToken)
            ?? throw new EntityNotFoundException($"Fine {fineId} was not found.");
        return profile.ToResponse(record);
    }

    private static FineType ParseFineType(string value)
    {
        var normalized = new string(value.Where(char.IsLetterOrDigit).ToArray()).ToLowerInvariant();
        return normalized switch
        {
            "bookdamage" => FineType.BookDamage,
            "latesubmission" => FineType.LateSubmission,
            _ => throw new ValidationException("Fine Type must be 'Book Damage' or 'Late Submission'.")
        };
    }

    private static decimal GetBookDamageAmount(decimal? requestedAmount)
    {
        if (requestedAmount is > 0)
        {
            throw new ValidationException("Book Damage fines are fixed at ₹0; do not provide an amount.");
        }

        return 0m;
    }

    private static decimal GetLateSubmissionAmount(decimal? requestedAmount)
    {
        if (requestedAmount is null or <= 0 or > 1_000_000m)
        {
            throw new ValidationException("A Late Submission fine amount greater than ₹0 and no more than ₹1,000,000 is required.");
        }

        if (requestedAmount.Value != decimal.Round(requestedAmount.Value, 2))
        {
            throw new ValidationException("Fine amounts can have no more than two decimal places.");
        }

        return requestedAmount.Value;
    }

    private static string ParsePaymentMethod(string value) => value.Trim().ToUpperInvariant() switch
    {
        "UPI" => "UPI",
        "CASH" => "Cash",
        _ => throw new ValidationException("PaymentMethod must be 'UPI' or 'Cash'.")
    };
}
