using System.ComponentModel.DataAnnotations;
using LibraryApi.Dtos.Members;
using LibraryApi.Infrastructure.Profiles;
using LibraryApi.Infrastructure.Repositories;
using LibraryApi.Infrastructure.Services.Interfaces;

namespace LibraryApi.Infrastructure.Services;

public sealed class MemberService(IMemberRepository members, MemberProfile profile) : IMemberService
{
    public async Task<IReadOnlyList<MemberResponse>> GetAllAsync(CancellationToken cancellationToken) =>
        (await members.GetAllAsync(cancellationToken)).Select(profile.ToResponse).ToArray();

    public async Task<MemberResponse> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        profile.ToResponse(await GetRequiredMemberAsync(id, cancellationToken));

    public async Task<MemberResponse> CreateAsync(
        CreateMemberRequest request,
        CancellationToken cancellationToken)
    {
        EnsureMemberFields(request.FullName, request.Email);
        var email = request.Email.Trim();
        if (await members.EmailExistsAsync(email, null, cancellationToken))
        {
            throw new ConflictException("A member with this email address already exists.");
        }

        var member = profile.ToEntity(request);
        await members.AddAsync(member, cancellationToken);
        return profile.ToResponse(member);
    }

    public async Task<MemberResponse> UpdateAsync(
        int id,
        UpdateMemberRequest request,
        CancellationToken cancellationToken)
    {
        EnsureMemberFields(request.FullName, request.Email);
        var member = await GetRequiredMemberAsync(id, cancellationToken);
        var email = request.Email.Trim();
        if (await members.EmailExistsAsync(email, id, cancellationToken))
        {
            throw new ConflictException("A member with this email address already exists.");
        }

        profile.Apply(request, member);
        await members.SaveChangesAsync(cancellationToken);
        return profile.ToResponse(member);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var member = await GetRequiredMemberAsync(id, cancellationToken);
        if (await members.HasLoansAsync(id, cancellationToken))
        {
            throw new ConflictException("A member with loan history cannot be deleted.");
        }

        await members.DeleteAsync(member, cancellationToken);
    }

    private async Task<Infrastructure.Entity.Member> GetRequiredMemberAsync(int id, CancellationToken cancellationToken) =>
        await members.GetByIdAsync(id, cancellationToken)
        ?? throw new EntityNotFoundException($"Member {id} was not found.");

    private static void EnsureMemberFields(string fullName, string email)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new ValidationException("Full name is required.");
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ValidationException("Email is required.");
        }
    }
}
