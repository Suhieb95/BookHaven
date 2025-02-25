using BookHaven.Domain.DTOs.Auth;
using BookHaven.Domain.DTOs.Users;
using BookHaven.Domain.Enums;
using BookHaven.Domain.Specification;
namespace BookHaven.Application.Interfaces.Services;
public interface IUserService : IGenericReadWithParamRepository<List<User>, Specification>, IGenericWriteRepository<InternalUserRegisterRequest, InternalUserUpdateRequest, Guid>
{
    Task LastLogin(string emailAddress, UserType personType = UserType.Internal);
    Task SaveEmailConfirmationToken(EmailConfirmationResult emailConfirmationResult, CancellationToken? cancellationToken);
    Task SavePassowordResetToken(ResetPasswordResult passwordResult, CancellationToken? cancellationToken);
    Task UpdatePassowordResetToken(PasswordChangeRequest passwordChangeRequest, CancellationToken? cancellationToken);
    Task UpdateEmailConfirmationToken(Guid id, CancellationToken? cancellationToken);
    Task<string[]> GetUserRoles(Guid id, CancellationToken? cancellationToken);
    Task<string[]> GetUserPermissions(Guid id, CancellationToken? cancellationToken);
    Task RemoveProfilePicture(Guid id, CancellationToken? cancellationToken);
}
