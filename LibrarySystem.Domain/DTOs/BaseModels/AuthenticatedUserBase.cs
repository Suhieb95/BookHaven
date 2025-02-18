namespace LibrarySystem.Domain.DTOs.BaseModels;

public abstract class AuthenticatedUserBase(string emailAddress, string userName, string imageurl, string token)
 : PersonBase(emailAddress, userName, imageurl), IToken
{
    public string Token { get; } = token;
};