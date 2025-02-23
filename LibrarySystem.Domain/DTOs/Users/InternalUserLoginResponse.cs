using LibrarySystem.Domain.BaseModels.User;
using LibrarySystem.Domain.DTOs.BaseModels;
using LibrarySystem.Domain.Enums;

namespace LibrarySystem.Domain.DTOs.Users;
public class InternalUserLoginResponse(string emailAddress, string userName, string token, string imageurl, IReadOnlyList<string> permissions, Guid id, string[] roles)
  : AuthenticatedUserBase(emailAddress, userName, token, id, PersonType.InternalUser), IUser
{
  public IReadOnlyList<string> Permissions { get; } = permissions;
  public string? ImageUrl { get; } = imageurl;
  public string[] Roles { get; } = roles;

};