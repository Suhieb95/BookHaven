using LibrarySystem.Domain.DTOs.Users;

namespace LibrarySystem.Application.Authentication.Users;
public interface IUserRegistrationService
{
    Task<Result<bool>> Register(InternalUserRegisterRequest request, CancellationToken? cancellationToken = null);
    Task<Result<bool>> ConfirmEmailAddress(Guid id, CancellationToken? cancellationToken = null);
}