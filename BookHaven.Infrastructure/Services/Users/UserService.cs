using BookHaven.Application.Helpers;
using BookHaven.Application.Interfaces.Database;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs;
using BookHaven.Domain.DTOs.Users;
using BookHaven.Domain.Enums;
using BookHaven.Infrastructure.Mappings.Person;

namespace BookHaven.Infrastructure.Services.Users;
public class UserService(ISqlDataAccess sqlDataAccess, IDateTimeProvider dateTimeProvider, IRedisCacheService redisCacheService, ICacheValidator cacheValidator)
    : GenericSpecificationReadRepository(sqlDataAccess), IUserService
{
    private readonly ISqlDataAccess _sqlDataAccess = sqlDataAccess;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    private readonly IRedisCacheService _redisCacheService = redisCacheService;
    private readonly ICacheValidator _cacheValidator = cacheValidator;
    public async Task<Guid> Add(InternalUserRegisterRequest request, CancellationToken? cancellationToken = null)
    {
        const string Sql = "SPCreateUser";
        Guid res = await _sqlDataAccess.SaveData<Guid>(Sql, request.ToParameter(_dateTimeProvider), StoredProcedure, cancellationToken);
        return res;
    }
    public async Task Delete(Guid id, CancellationToken? cancellationToken)
     => await _sqlDataAccess.SaveData("DELETE FROM Users WHERE Id = @Id", new { Id = id }, cancellationToken: cancellationToken);
    public async Task Update(InternalUserUpdateRequest request, CancellationToken? cancellationToken = null)
    {
        const string Sql = "SPUpdateUser";
        await _sqlDataAccess.SaveData(Sql, request.ToParameter(), StoredProcedure, cancellationToken);
    }
    public async Task LastLogin(string emailAddress, UserType personType = UserType.Internal)
    {
        string sql = $"UPDATE {personType.GetEnumValue()} SET LastLogin = @LastLogin WHERE EmailAddress = @EmailAddress";
        await _sqlDataAccess.SaveData(sql, new { EmailAddress = emailAddress, LastLogin = _dateTimeProvider.UtcNow });
    }
    public async Task<string[]> GetUserRoles(Guid id, CancellationToken? cancellationToken)
    {
        const string Sql = "SPGetUserRoles";
        string key = $"{id}-roles";
        List<string> rolesResult = await _cacheValidator.Validate(key,
          async () => await _sqlDataAccess.LoadData<string>(Sql, new { id }, StoredProcedure, cancellationToken),
          TimeSpan.FromDays(7));
        return [.. rolesResult];
    }
    public async Task<string[]> GetUserPermissions(Guid id, CancellationToken? cancellationToken)
    {
        const string Sql = "SPGetUserPermissions";
        string key = $"{id}-permissions";
        List<string>? permissions = await _cacheValidator.Validate(key,
          async () => await _sqlDataAccess.LoadData<string>(Sql, new { id }, StoredProcedure, cancellationToken),
          TimeSpan.FromDays(7));
        return [.. permissions];
    }
}