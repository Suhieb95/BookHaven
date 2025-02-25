using BookHaven.Domain.DTOs.Customers;
namespace BookHaven.Application.Authentication.Customers;
public interface ICustomerRegistrationService
{
    Task<Result<bool>> Register(CustomerRegisterRequest customerRegisterRequest, CancellationToken? cancellationToken = null);
    Task<Result<bool>> ConfirmEmailAddress(Guid id, CancellationToken? cancellationToken = null);
}