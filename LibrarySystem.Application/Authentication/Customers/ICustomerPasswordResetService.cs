using LibrarySystem.Domain.DTOs.Auth;
namespace LibrarySystem.Application.Authentication.Customers;
public interface ICustomerPasswordResetService
{
    Task<Result<bool>> ResetPassword(string emailAddress, CancellationToken? cancellationToken = null);
    Task<Result<bool>> ChangePassword(PasswordChangeRequest request, CancellationToken? cancellationToken = null);
    Task<Result<bool>> VerifyToken(Guid id, CancellationToken? cancellationToken = null);
}