using LibrarySystem.Domain.DTOs.Auth;
namespace LibrarySystem.Application.Authentication.Users;
public class UserResetPassword(IUnitOfWork _unitOfWork) : IUserResetPassword
{
    public async Task<Result<bool>> ChangePassword(PasswordChangeRequest request, CancellationToken? cancellationToken = null)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> GenerateResetPasswordLink(string emailAddress, CancellationToken? cancellationToken = null)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> ResetPassword(string emailAddress, CancellationToken? cancellationToken = null)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> VerifyToken(Guid id, CancellationToken? cancellationToken = null)
    {
        throw new NotImplementedException();
    }
}