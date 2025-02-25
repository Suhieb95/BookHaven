using BookHaven.Domain.BaseModels.User;
using BookHaven.Domain.Enums;

namespace BookHaven.Domain.DTOs.Users;
public class InternalUserLoginResponse(string emailAddress, string userName, string token, Guid id, string refreshToken, string? imageurl)
  : AuthenticatedUserBase(emailAddress, userName, token, id, UserType.Internal, imageurl, refreshToken)
{

};