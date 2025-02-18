using LibrarySystem.Domain.DTOs.BaseModels;

namespace LibrarySystem.Domain.DTOs.Users;
public class InternalUserLoginResponse(string emailAddress, string userName, string token, string imageurl, IReadOnlyList<string> permissions)
  : AuthenticatedUserBase(emailAddress, userName, imageurl, token), IUser
{
    public IReadOnlyList<string> Permissions { get; } = permissions;
};