using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Dtos.Members;

public sealed class UpdateMemberRequest
{
    [Required, StringLength(150, MinimumLength = 1)]
    public string FullName { get; init; } = string.Empty;

    [Required, EmailAddress, StringLength(254)]
    public string Email { get; init; } = string.Empty;
}
