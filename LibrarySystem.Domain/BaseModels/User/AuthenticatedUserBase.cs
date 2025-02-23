using LibrarySystem.Domain.DTOs.BaseModels;

namespace LibrarySystem.Domain.BaseModels.User;
public abstract class AuthenticatedUserBase(string emailAddress, string userName, string token, Guid id)
 : PersonBase(emailAddress, userName, id), IToken
{
    public string Token { get; } = token;
};