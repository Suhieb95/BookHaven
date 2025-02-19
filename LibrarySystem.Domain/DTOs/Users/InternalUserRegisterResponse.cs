using LibrarySystem.Domain.BaseModels.User;
using LibrarySystem.Domain.DTOs.BaseModels;

namespace LibrarySystem.Domain.DTOs.Users;
public class InternalUserRegisterResponse(string emailAddress, string userName, string token, string imageurl, IReadOnlyList<string> permissions, Guid id)
  : AuthenticatedUserBase(emailAddress, userName, imageurl, token, id), IUser
{
  public IReadOnlyList<string> Permissions { get; } = permissions;
};
