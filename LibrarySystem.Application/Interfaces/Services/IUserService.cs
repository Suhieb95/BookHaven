using LibrarySystem.Domain.DTOs.Auth;
using LibrarySystem.Domain.DTOs.Users;
using LibrarySystem.Domain.Enums;
using LibrarySystem.Domain.Specification;
namespace LibrarySystem.Application.Interfaces.Services;
public interface IUserService : IGenericReadWithParamRepository<List<User>, Specification>, IGenericWriteRepository<InternalUserRegisterRequest, InternalUserUpdateRequest, Guid>
{
    Task LastLogin(string emailAddress, PersonType personType = PersonType.InternalUser);
    Task SaveEmailConfirmationToken(EmailConfirmationResult emailConfirmationResult, CancellationToken? cancellationToken);
    Task SavePassowordResetToken(ResetPasswordResult passwordResult, CancellationToken? cancellationToken);
    Task UpdatePassowordResetToken(PasswordChangeRequest passwordChangeRequest, CancellationToken? cancellationToken);
    Task UpdateEmailConfirmationToken(Guid id, CancellationToken? cancellationToken);
    Task<string[]> GetUserRoles(Guid id, CancellationToken? cancellationToken);
    Task<string[]> GetUserPermissions(Guid id, CancellationToken? cancellationToken);
}
