using BookHaven.Domain.DTOs.Users;
using BookHaven.Domain.Enums;
namespace BookHaven.Application.Interfaces.Services;
public interface IUserService : IGenericSpecificationReadRepository, IGenericWriteRepository<InternalUserRegisterRequest, InternalUserUpdateRequest, Guid>
{
    Task<string[]> GetUserRoles(Guid id, CancellationToken? cancellationToken);
    Task<string[]> GetUserPermissions(Guid id, CancellationToken? cancellationToken);
    Task LastLogin(string emailAddress, UserType personType = UserType.Internal);
}