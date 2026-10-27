using LibraryApi.Dtos.Loans;

namespace LibraryApi.Infrastructure.Services.Interfaces;

public interface ILoanService
{
    Task<IReadOnlyList<LoanResponse>> GetAllAsync(CancellationToken cancellationToken);
    Task<LoanResponse> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<LoanResponse> CheckoutAsync(CheckoutLoanRequest request, CancellationToken cancellationToken);
    Task<LoanResponse> ReturnAsync(int id, CancellationToken cancellationToken);
}
