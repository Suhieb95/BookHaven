using LibrarySystem.Domain.DTOs.Customers;
namespace LibrarySystem.Application.Authentication.Customers;
public interface ICustomerUpdateService
{
    Task<Result<bool>> Update(CustomerUpdateRequest request, CancellationToken? cancellationToken = default);
    Task<Result<bool>> RemoveProfilePicture(Guid id, CancellationToken? cancellationToken = default);
}