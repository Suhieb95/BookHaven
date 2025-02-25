using BookHaven.Domain.DTOs.BaseModels;
using BookHaven.Domain.Enums;

namespace BookHaven.Domain.BaseModels.User;
public abstract class AuthenticatedUserBase(string emailAddress, string userName, string token, Guid id, UserType personType, string? imageurl, string refreshToken)
 : PersonBase(emailAddress, userName, id, imageurl), IToken
{
    public string Token { get; } = token;
    public string RefreshToken { get; init; } = refreshToken;
    public UserType PersonType { get; } = personType;
};