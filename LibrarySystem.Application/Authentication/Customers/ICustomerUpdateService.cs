using LibrarySystem.Domain.DTOs.Customers;
namespace LibrarySystem.Application.Authentication.Customers;
public interface ICustomerUpdateService
{
    Task<Result<bool>> Update(CustomerUpdateRequest request, CancellationToken? cancellationToken = default);
}