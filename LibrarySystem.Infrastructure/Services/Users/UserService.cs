using LibrarySystem.Application.Helpers;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Application.Interfaces.Database;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs.Auth;
using LibrarySystem.Domain.DTOs.Users;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Enums;
using LibrarySystem.Domain.Specification;
using LibrarySystem.Infrastructure.Mappings.Person;
namespace LibrarySystem.Infrastructure.Services.Users;
public class UserService(ISqlDataAccess _sqlDataAccess, IDateTimeProvider _dateTimeProvider) : IUserService
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
    public async Task<List<User>> GetAll(Specification param, CancellationToken? cancellationToken = null)
        => await _sqlDataAccess.LoadData<User>(param.ToSql(), param.Parameters, cancellationToken: cancellationToken);
    public async Task LastLogin(string emailAddress, PersonType personType = PersonType.InternalUser)
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
        const string Sql = @"UPDATE Users SET ResetPasswordTokenExpiry = NULL, ResetPasswordToken = NULL, Password = @Password WHERE Id = @Id";
        await _sqlDataAccess.SaveData(Sql, request.ToParameter(), cancellationToken: cancellationToken);
    }
    public async Task UpdateEmailConfirmationToken(Guid id, CancellationToken? cancellationToken)
    {
        const string Sql = @"UPDATE Users SET VerifyEmailTokenExpiry = NULL, VerifyEmailToken = NULL, IsActive = 1, IsVerified = 1 WHERE Id = @Id";
        await _sqlDataAccess.SaveData(Sql, new { Id = id }, cancellationToken: cancellationToken);
    }
}