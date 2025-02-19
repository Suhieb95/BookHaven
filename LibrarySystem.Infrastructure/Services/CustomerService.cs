using LibrarySystem.Application.Interfaces.Database;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs.Customers;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Specification;

namespace LibrarySystem.Infrastructure.Services;
public class CustomerService(ISqlDataAccess _sqlDataAccess) : ICustomerService
{
    public async Task<Guid> Add(CustomerRegisterRequest entity)
    {
        throw new NotImplementedException();
    }
    public async Task Delete(Guid id)
        => await _sqlDataAccess.SaveData("DELETE FROM Customers WHERE Id = @Id", new { Id = id });
    public async Task Update(CustomerUpdateRequest entity)
    {
        throw new NotImplementedException();
    }
    public async Task<List<Customer>> GetAll(Specification specification, CancellationToken? cancellationToken = null)
        => await _sqlDataAccess.LoadData<Customer>(specification.ToSql(), specification.Parameters, cancellationToken: cancellationToken);
}
