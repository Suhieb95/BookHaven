using BookHaven.Application.Interfaces.Database;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs.Customers;
using BookHaven.Infrastructure.Mappings.Person;
namespace BookHaven.Infrastructure.Services.Customers;
public class CustomerService(ISqlDataAccess sqlDataAccess, IDateTimeProvider dateTimeProvider)
         : GenericSpecificationReadRepository(sqlDataAccess), ICustomerService
{
    private readonly ISqlDataAccess _sqlDataAccess = sqlDataAccess;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
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
        await _sqlDataAccess.SaveData(Sql, entity.ToParameter(), StoredProcedure, cancellationToken);
    }
}