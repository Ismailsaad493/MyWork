using LibraryApi.Infrastructure.Entity;

namespace LibraryApi.Infrastructure.Repositories;

public interface IMemberRepository
{
    Task<IReadOnlyList<Member>> GetAllAsync(CancellationToken cancellationToken);
    Task<Member?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> EmailExistsAsync(string email, int? excludingMemberId, CancellationToken cancellationToken);
    Task<bool> HasLoansAsync(int memberId, CancellationToken cancellationToken);
    Task AddAsync(Member member, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task DeleteAsync(Member member, CancellationToken cancellationToken);
}
