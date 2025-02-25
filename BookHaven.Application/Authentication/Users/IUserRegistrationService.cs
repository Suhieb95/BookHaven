using BookHaven.Domain.DTOs.Users;

namespace BookHaven.Application.Authentication.Users;
public interface IUserRegistrationService
{
    Task<Result<bool>> Register(InternalUserRegisterRequest request, CancellationToken? cancellationToken = null);
    Task<Result<bool>> ConfirmEmailAddress(Guid id, CancellationToken? cancellationToken = null);
}