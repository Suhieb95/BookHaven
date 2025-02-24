using LibrarySystem.Domain.DTOs.Users;
namespace LibrarySystem.Application.Authentication.Users;
public interface IUserLoginService
{
    Task<Result<InternalUserLoginResponse>> Login(InternalUserLoginRequest request, CancellationToken? cancellationToken = null);
}