using LibrarySystem.Domain.DTOs.Customers;
using LibrarySystem.Domain.Entities;
namespace LibrarySystem.Application.Authentication.Customers;
public interface ICustomerRegistrationService
{
    Task<Result<bool>> Register(CustomerRegisterRequest customerRegisterRequest, CancellationToken? cancellationToken = null);
    Task<Result<bool> ConfirmEmailAddress(Guid id, CancellationToken? cancellationToken = null);
}