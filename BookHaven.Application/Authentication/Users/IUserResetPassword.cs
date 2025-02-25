using BookHaven.Domain.DTOs.Auth;

namespace BookHaven.Application.Authentication.Users;

public interface IUserResetPassword
{
    Task<Result<bool>> ResetPassword(string emailAddress, CancellationToken? cancellationToken = null);
    Task<Result<bool>> ChangePassword(PasswordChangeRequest request, CancellationToken? cancellationToken = null);
    Task<Result<bool>> VerifyToken(Guid id, CancellationToken? cancellationToken = null);
}