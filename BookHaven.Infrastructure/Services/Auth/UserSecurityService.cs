using BookHaven.Application.Interfaces.Database;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs.Auth;
using BookHaven.Domain.Enums;
using BookHaven.Infrastructure.Mappings.Person;

namespace BookHaven.Infrastructure.Services.Auth;
public class UserSecurityService(ISqlDataAccess _sqlDataAccess) : IUserSecurityService
{
    public async Task SaveEmailConfirmationToken(EmailConfirmationResult emailConfirmationResult, CancellationToken? cancellationToken, UserType userType = UserType.Customer)
    {
        string sql = $@"UPDATE {GetTableName(userType)} SET VerifyEmailTokenExpiry = @VerifyEmailTokenExpiry, VerifyEmailToken = @VerifyEmailToken WHERE Id = @Id";
        await _sqlDataAccess.SaveData(sql, emailConfirmationResult.ToParameter(), cancellationToken: cancellationToken);
    }
    public async Task SavePassowordResetToken(ResetPasswordResult passwordResult, CancellationToken? cancellationToken, UserType userType = UserType.Customer)
    {
        string sql = $@"UPDATE {GetTableName(userType)} SET ResetPasswordTokenExpiry = @ResetPasswordTokenExpiry, ResetPasswordToken = @ResetPasswordToken WHERE TRIM(LOWER(EmailAddress)) = TRIM(LOWER(@EmailAddress))";
        await _sqlDataAccess.SaveData(sql, passwordResult.ToParameter(), cancellationToken: cancellationToken);
    }
    public async Task UpdatePassowordResetToken(PasswordChangeRequest request, CancellationToken? cancellationToken, UserType userType = UserType.Customer)
    {
        string sql = userType switch
        {
            UserType.Customer => @"UPDATE Customers SET ResetPasswordTokenExpiry = NULL, ResetPasswordToken = NULL, Password = @Password WHERE Id = @Id",
            UserType.Internal => @"UPDATE Users SET ResetPasswordTokenExpiry = NULL, ResetPasswordToken = NULL, Password = @Password, IsActive = 1 WHERE Id = @Id",
            _ => throw new Exception("Invalid User.")
        };
        await _sqlDataAccess.SaveData(sql, request.ToParameter(), cancellationToken: cancellationToken);
    }
    public async Task UpdateEmailConfirmationToken(Guid id, CancellationToken? cancellationToken, UserType userType = UserType.Customer)
    {
        string sql = userType switch
        {
            UserType.Customer => @"UPDATE Customers SET VerifyEmailTokenExpiry = NULL, VerifyEmailToken = NULL, IsActive = 1, IsVerified = 1 WHERE Id = @Id",
            UserType.Internal => @"UPDATE Users SET VerifyEmailTokenExpiry = NULL, VerifyEmailToken = NULL, IsVerified = 1 WHERE Id = @Id",
            _ => throw new Exception("Invalid User.")
        };
        await _sqlDataAccess.SaveData(sql, new { Id = id }, cancellationToken: cancellationToken);
    }
    public async Task RemoveProfilePicture(Guid id, CancellationToken? cancellationToken, UserType userType = UserType.Customer)
    {
        string sql = $@"UPDATE {GetTableName(userType)} SET ImageUrl = NULL WHERE Id = @Id";
        await _sqlDataAccess.SaveData(sql, new { id }, cancellationToken: cancellationToken);
    }
    private static string GetTableName(UserType userType) => userType switch
    {
        UserType.Customer => "Customers",
        UserType.Internal => "Users",
        _ => throw new Exception("Invalid User.")
    };
}