namespace LibraryApi.Dtos.Fines;

public sealed record FineResponse(
    int Id,
    string FineType,
    decimal Amount,
    int LoanId,
    int BookId,
    string BookTitle,
    int BorrowerId,
    string BorrowerName,
    DateTime CreatedAtUtc,
    DateOnly? PaymentDate,
    string? PaymentMethod);
