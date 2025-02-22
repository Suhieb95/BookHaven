using LibrarySystem.Application.Interfaces;
using LibrarySystem.Application.Interfaces.Database;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs.Auth;
using LibrarySystem.Domain.DTOs.Customers;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Specification;
using LibrarySystem.Infrastructure.Mappings.Customers;
namespace LibrarySystem.Infrastructure.Services.CustomerService;
public class CustomerService(ISqlDataAccess _sqlDataAccess, IDateTimeProvider _dateTimeProvider) : ICustomerService
{
    public async Task<Guid> Add(CustomerRegisterRequest request, CancellationToken? cancellationToken)
    {
        const string Sql = "SPCreateCustomer";
        Guid res = await _sqlDataAccess.SaveData<Guid>(Sql, request.ToParameter(_dateTimeProvider), StoredProcedure, cancellationToken);
        return res;
    }
    public async Task Delete(Guid id, CancellationToken? cancellationToken)
        => await _sqlDataAccess.SaveData("DELETE FROM Customers WHERE Id = @Id", new { Id = id }, cancellationToken: cancellationToken);
    public async Task Update(CustomerUpdateRequest entity, CancellationToken? cancellationToken = null)
    {
        const string Sql = "SPUpdateCustomer";
        await _sqlDataAccess.SaveData(Sql, entity.ToParameter, StoredProcedure, cancellationToken);
    }
    public async Task<List<Customer>> GetAll(Specification specification, CancellationToken? cancellationToken = null)
        => await _sqlDataAccess.LoadData<Customer>(specification.ToSql(), specification.Parameters, cancellationToken: cancellationToken);
    public async Task SaveEmailConfirmationToken(EmailConfirmationResult emailConfirmationResult, CancellationToken? cancellationToken)
    {
        const string Sql = @"UPDATE Customers SET VerifyEmailTokenExpiry = @VerifyEmailTokenExpiry, VerifyEmailToken = @VerifyEmailToken
                                    WHERE Id = @Id";
        await _sqlDataAccess.SaveData(Sql, emailConfirmationResult.ToParameter, cancellationToken: cancellationToken);
    }
    public async Task SavePassowordResetToken(ResetPasswordResult passwordResult, CancellationToken? cancellationToken)
    {
        const string Sql = @"UPDATE Customers SET PasswordResetTokenExpiry = @PasswordResetTokenExpiry, PasswordResetToken = @PasswordResetToken
                                    WHERE TRIM(LOWER(EmailAddress)) = TRIM(LOWER(@EmailAddress))";
        await _sqlDataAccess.SaveData(Sql, passwordResult.ToParameter, cancellationToken: cancellationToken);
    }
    public async Task UpdatePassowordResetToken(PasswordChangeRequest request, CancellationToken? cancellationToken)
    {
        const string Sql = @"UPDATE Customers SET PasswordResetTokenExpiry = NULL, PasswordResetToken = NULL, Password = @Password
                                    WHERE Id = @UserId";
        await _sqlDataAccess.SaveData(Sql, request.ToParameter, cancellationToken: cancellationToken);
    }
    public async Task UpdateEmailConfirmationToken(Guid id, CancellationToken? cancellationToken)
    {
        const string Sql = @"UPDATE Customers SET VerifyEmailTokenExpiry = NULL, VerifyEmailToken = NULL, 
                            IsActive = 1, IsVerified = 1 WHERE Id = @Id";
        await _sqlDataAccess.SaveData(Sql, new { Id = id }, cancellationToken: cancellationToken);
    }
}