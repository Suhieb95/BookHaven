using BookHaven.Domain.DTOs.Auth;
using BookHaven.Domain.Enums;

namespace BookHaven.Application.Interfaces.Services;
public interface IUserSecurityService
{
    Task SaveEmailConfirmationToken(EmailConfirmationResult emailConfirmationResult, CancellationToken? cancellationToken, UserType userType = UserType.Customer);
    Task SavePassowordResetToken(ResetPasswordResult passwordResult, CancellationToken? cancellationToken, UserType userType = UserType.Customer);
    Task UpdatePassowordResetToken(PasswordChangeRequest passwordChangeRequest, CancellationToken? cancellationToken, UserType userType = UserType.Customer);
    Task UpdateEmailConfirmationToken(Guid id, CancellationToken? cancellationToken, UserType userType = UserType.Customer);
    Task RemoveProfilePicture(Guid id, CancellationToken? cancellationToken, UserType userType = UserType.Customer);
}