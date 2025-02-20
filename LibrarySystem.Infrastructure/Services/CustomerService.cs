using LibrarySystem.Application.Interfaces.Database;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs.Auth;
using LibrarySystem.Domain.DTOs.Customers;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Specification;

namespace LibrarySystem.Infrastructure.Services;
public class CustomerService(ISqlDataAccess _sqlDataAccess) : ICustomerService
{
    public async Task<Guid> Add(CustomerRegisterRequest request)
    {
        const string Sql = "SPCreateCustomer";
        var param = new
        {
            Id = Guid.NewGuid(),
            request.UserName,
            request.EmailAddress,
            request.Password,
            IsActive = false,
            IsVerified = false
        };

        Guid res = await _sqlDataAccess.SaveData<Guid>(Sql, param, StoredProcedure);
        return res;
    }
    public async Task Delete(Guid id)
        => await _sqlDataAccess.SaveData("DELETE FROM Customers WHERE Id = @Id", new { Id = id });
    public async Task Update(CustomerUpdateRequest entity)
    {
        const string Sql = "SPUpdateCustomer";
        var param = new
        {
            entity.EmailAddress,
            entity.Password,
            entity.UserName,
            entity.ImageUrl
        };
        await _sqlDataAccess.SaveData(Sql, param, StoredProcedure);
    }
    public async Task<List<Customer>> GetAll(Specification specification, CancellationToken? cancellationToken = null)
        => await _sqlDataAccess.LoadData<Customer>(specification.ToSql(), specification.Parameters, cancellationToken: cancellationToken);

    public async Task SaveEmailConfirmationToken(EmailConfirmationResult emailConfirmationResult)
    {
        const string Sql = @"UPDATE Customers SET VerifyEmailTokenExpiry = @VerifyEmailTokenExpiry, VerifyEmailToken = @VerifyEmailToken
                                    WHERE Id = @Id";
        var param = new
        {
            Id = emailConfirmationResult.UserId,
            VerifyEmailToken = emailConfirmationResult.EmailAddressConfirmationToken,
            VerifyEmailTokenExpiry = emailConfirmationResult.EmailAddressConfirmationTokenExpiry,
        };

        await _sqlDataAccess.SaveData(Sql, param);
    }
    public async Task SavePassowordResetToken(ResetPasswordResult passwordResult)
    {
        const string Sql = @"UPDATE Customers SET PasswordResetTokenExpiry = @PasswordResetTokenExpiry, PasswordResetToken = @PasswordResetToken
                                    WHERE TRIM(LOWER(EmailAddress)) = TRIM(LOWER(@EmailAddress))";
        var param = new
        {
            passwordResult.EmailAddress,
            passwordResult.PasswordResetToken,
            passwordResult.PasswordResetTokenExpiry,
        };
        await _sqlDataAccess.SaveData(Sql, param);
    }
    public async Task UpdatePassowordResetToken(Guid id)
    {
        const string Sql = @"UPDATE Customers SET VerifyEmailTokenExpiry = NULL, VerifyEmailToken = NULL
                                    WHERE Id = @Id";
        await _sqlDataAccess.SaveData(Sql, new { Id = id });
    }
    public async Task UpdateEmailConfirmationToken(PasswordChangeRequest passwordChangeRequest)
    {
        const string Sql = @"UPDATE Customers SET PasswordResetTokenExpiry = NULL, PasswordResetToken = NULL, Password = @Password
                                    WHERE Id = @UserId";
        var param = new
        {
            passwordChangeRequest.UserId,
            passwordChangeRequest.Password,
        };
        await _sqlDataAccess.SaveData(Sql, param);
    }
}