using LibrarySystem.Domain.BaseModels.User;

namespace LibrarySystem.Domain.DTOs.Users;
public class InternalUserRegisterResponse(string emailAddress, string userName, string token, string imageurl, IReadOnlyList<string> permissions)
  : AuthenticatedUserBase(emailAddress, userName, imageurl, token), IUser
{
  public IReadOnlyList<string> Permissions { get; } = permissions;
};
