using LibraryApi.Dtos.Members;

namespace LibraryApi.Infrastructure.Services.Interfaces;

public interface IMemberService
{
    Task<IReadOnlyList<MemberResponse>> GetAllAsync(CancellationToken cancellationToken);
    Task<MemberResponse> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<MemberResponse> CreateAsync(CreateMemberRequest request, CancellationToken cancellationToken);
    Task<MemberResponse> UpdateAsync(int id, UpdateMemberRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}
