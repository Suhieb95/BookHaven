using BookHaven.Application.Helpers;
using BookHaven.Application.Interfaces.Database;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs.Auth;
using BookHaven.Domain.DTOs.Users;
using BookHaven.Domain.Enums;
using BookHaven.Infrastructure.Mappings.Person;

namespace BookHaven.Infrastructure.Services.Users;
public class UserService(ISqlDataAccess _sqlDataAccess, IDateTimeProvider _dateTimeProvider, IRedisCacheService _redisCacheService) : GenericSpecificationReadRepository(_sqlDataAccess), IUserService
{
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
    public async Task SaveEmailConfirmationToken(EmailConfirmationResult emailConfirmationResult, CancellationToken? cancellationToken)
    {
        const string Sql = @"UPDATE Users SET VerifyEmailTokenExpiry = @VerifyEmailTokenExpiry, VerifyEmailToken = @VerifyEmailToken WHERE Id = @Id";
        await _sqlDataAccess.SaveData(Sql, emailConfirmationResult.ToParameter(), cancellationToken: cancellationToken);
    }
    public async Task SavePassowordResetToken(ResetPasswordResult passwordResult, CancellationToken? cancellationToken)
    {
        const string Sql = @"UPDATE Users SET ResetPasswordTokenExpiry = @ResetPasswordTokenExpiry, ResetPasswordToken = @ResetPasswordToken WHERE TRIM(LOWER(EmailAddress)) = TRIM(LOWER(@EmailAddress))";
        await _sqlDataAccess.SaveData(Sql, passwordResult.ToParameter(), cancellationToken: cancellationToken);
    }
    public async Task UpdatePassowordResetToken(PasswordChangeRequest request, CancellationToken? cancellationToken)
    {
        const string Sql = @"UPDATE Users SET ResetPasswordTokenExpiry = NULL, ResetPasswordToken = NULL, Password = @Password, IsActive = 1 WHERE Id = @Id";
        await _sqlDataAccess.SaveData(Sql, request.ToParameter(), cancellationToken: cancellationToken);
    }
    public async Task UpdateEmailConfirmationToken(Guid id, CancellationToken? cancellationToken)
    {
        const string Sql = @"UPDATE Users SET VerifyEmailTokenExpiry = NULL, VerifyEmailToken = NULL, IsVerified = 1 WHERE Id = @Id";
        await _sqlDataAccess.SaveData(Sql, new { Id = id }, cancellationToken: cancellationToken);
    }
    public async Task<string[]> GetUserRoles(Guid id, CancellationToken? cancellationToken)
    {
        string key = $"{id}-roles";
        string[]? permissions = await _redisCacheService.Get<string[]>(key);
        if (permissions is not null && permissions.Length > 0)
            return permissions;

        const string Sql = "SPGetUserRoles";
        List<string>? res = await _sqlDataAccess.LoadData<string>(Sql, new { id }, StoredProcedure, cancellationToken);
        if (res.Count > 0)
            await _redisCacheService.Set(key, res, TimeSpan.FromDays(7));

        return [.. res];
    }
    public async Task<string[]> GetUserPermissions(Guid id, CancellationToken? cancellationToken)
    {
        string key = $"{id}-permissions";
        string[]? permissions = await _redisCacheService.Get<string[]>(key);
        if (permissions is not null && permissions.Length > 0)
            return permissions;

        const string Sql = "SPGetUserPermissions";
        List<string>? res = await _sqlDataAccess.LoadData<string>(Sql, new { id }, StoredProcedure, cancellationToken);
        if (res.Count > 0)
            await _redisCacheService.Set(key, res, TimeSpan.FromDays(7));

        return [.. res];
    }
    public async Task RemoveProfilePicture(Guid id, CancellationToken? cancellationToken)
    {
        const string Sql = @"UPDATE Users SET ImageUrl = NULL WHERE Id = @Id";
        await _sqlDataAccess.SaveData(Sql, new { id }, cancellationToken: cancellationToken);
    }
}