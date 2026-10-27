using LibraryApi.Dtos.Fines;

namespace LibraryApi.Infrastructure.Services.Interfaces;

public interface IFineService
{
    Task<IReadOnlyList<FineResponse>> GetAllAsync(CancellationToken cancellationToken);
    Task<FineResponse> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<FineResponse> CreateAsync(CreateFineRequest request, CancellationToken cancellationToken);
    Task<FineResponse> RecordPaymentAsync(
        int fineId,
        RecordFinePaymentRequest request,
        CancellationToken cancellationToken);
}
