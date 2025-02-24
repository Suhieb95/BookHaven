using LibrarySystem.Domain.BaseModels.User;
using LibrarySystem.Domain.Enums;

namespace LibrarySystem.Domain.DTOs.Users;
public class InternalUserLoginResponse(string emailAddress, string userName, string? imageurl, string token, Guid id)
  : AuthenticatedUserBase(emailAddress, userName, token, id, PersonType.InternalUser)
{
  public string? ImageUrl { get; } = imageurl;

};