using BookHaven.Domain.DTOs.Customers;
namespace BookHaven.Application.Authentication.Customers;
public interface ICustomerUpdateService
{
    Task<Result<bool>> Update(CustomerUpdateRequest request, CancellationToken? cancellationToken = default);
    Task<Result<bool>> RemoveProfilePicture(Guid id, CancellationToken? cancellationToken = default);
}