using LibrarySystem.Domain.DTOs.Users;
namespace LibrarySystem.Application.Authentication.Users;
public interface IUserUpdateService
{
    Task<Result<bool>> Update(InternalUserUpdateRequest request, CancellationToken? cancellationToken = default);
    Task<Result<bool>> RemoveProfilePicture(Guid id, CancellationToken? cancellationToken = default);
}