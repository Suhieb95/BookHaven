using LibrarySystem.Domain.BaseModels.User;
using LibrarySystem.Domain.Enums;

namespace LibrarySystem.Domain.DTOs.Users;
public class InternalUserLoginResponse(string emailAddress, string userName, string token, Guid id, string refreshToken, string? imageurl)
  : AuthenticatedUserBase(emailAddress, userName, token, id, UserType.Internal, imageurl, refreshToken)
{

};