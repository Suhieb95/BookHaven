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
        Guid id = Guid.NewGuid();

        var param = new
        {
            Id = id,
            request.UserName,
            request.EmailAddress,
            request.Password,
            IsActive = false,
            IsVerified = false
        };

        await _sqlDataAccess.SaveData<int>(Sql, param, StoredProcedure);
        return id;
    }
    public async Task Delete(Guid id)
        => await _sqlDataAccess.SaveData("DELETE FROM Customers WHERE Id = @Id", new { Id = id });
    public async Task Update(CustomerUpdateRequest entity)
    {
        throw new NotImplementedException();
    }
    public async Task<List<Customer>> GetAll(Specification specification, CancellationToken? cancellationToken = null)
        => await _sqlDataAccess.LoadData<Customer>(specification.ToSql(), specification.Parameters, cancellationToken: cancellationToken);

    public async Task SaveConfirmationToken(EmailConfirmationResult emailConfirmationResult)
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
}
