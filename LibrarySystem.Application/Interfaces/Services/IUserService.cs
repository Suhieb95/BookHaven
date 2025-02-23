using LibrarySystem.Domain.Enums;

namespace LibrarySystem.Application.Interfaces.Services;

public interface IUserService
{
    Task LastLogin(string emailAddress, PersonType personType = PersonType.InternalUser);
}
