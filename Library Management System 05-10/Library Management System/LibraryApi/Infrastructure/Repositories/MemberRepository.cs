using LibraryApi.Infrastructure.Context;
using LibraryApi.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Infrastructure.Repositories;

public sealed class MemberRepository(LibraryDbContext context) : IMemberRepository
{
    public async Task<IReadOnlyList<Member>> GetAllAsync(CancellationToken cancellationToken) =>
        await context.Members.AsNoTracking().OrderBy(member => member.FullName).ThenBy(member => member.Id)
            .ToListAsync(cancellationToken);

    public Task<Member?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        context.Members.SingleOrDefaultAsync(member => member.Id == id, cancellationToken);

    public Task<bool> EmailExistsAsync(string email, int? excludingMemberId, CancellationToken cancellationToken) =>
        excludingMemberId is int memberId
            ? context.Members.AnyAsync(
                member => member.Email == email && member.Id != memberId,
                cancellationToken)
            : context.Members.AnyAsync(member => member.Email == email, cancellationToken);

    public Task<bool> HasLoansAsync(int memberId, CancellationToken cancellationToken) =>
        context.Loans.AnyAsync(loan => loan.MemberId == memberId, cancellationToken);

    public async Task AddAsync(Member member, CancellationToken cancellationToken)
    {
        await context.Members.AddAsync(member, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken) =>
        context.SaveChangesAsync(cancellationToken);

    public async Task DeleteAsync(Member member, CancellationToken cancellationToken)
    {
        context.Members.Remove(member);
        await context.SaveChangesAsync(cancellationToken);
    }
}
