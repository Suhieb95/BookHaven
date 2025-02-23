using LibrarySystem.Domain.DTOs.BaseModels;
using LibrarySystem.Domain.Enums;

namespace LibrarySystem.Domain.BaseModels.User;
public abstract class AuthenticatedUserBase(string emailAddress, string userName, string token, Guid id, PersonType personType)
 : PersonBase(emailAddress, userName, id), IToken
{
    public string Token { get; } = token;
    public PersonType PersonType { get; } = personType;
};