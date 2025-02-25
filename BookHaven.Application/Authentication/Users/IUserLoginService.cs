using BookHaven.Domain.DTOs.Users;
namespace BookHaven.Application.Authentication.Users;
public interface IUserLoginService
{
    Task<Result<InternalUserLoginResponse>> Login(InternalUserLoginRequest request, CancellationToken? cancellationToken = default);
}