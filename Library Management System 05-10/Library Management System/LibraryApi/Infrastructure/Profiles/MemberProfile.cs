using LibraryApi.Dtos.Members;
using LibraryApi.Infrastructure.Entity;

namespace LibraryApi.Infrastructure.Profiles;

public sealed class MemberProfile
{
    public Member ToEntity(CreateMemberRequest request) => new()
    {
        FullName = request.FullName.Trim(),
        Email = request.Email.Trim()
    };

    public MemberResponse ToResponse(Member member) =>
        new(member.Id, member.FullName, member.Email);

    public void Apply(UpdateMemberRequest request, Member member)
    {
        member.FullName = request.FullName.Trim();
        member.Email = request.Email.Trim();
    }
}
