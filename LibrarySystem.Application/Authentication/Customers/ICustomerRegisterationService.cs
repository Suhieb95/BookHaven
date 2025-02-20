using LibrarySystem.Domain.DTOs.Customers;
using LibrarySystem.Domain.Entities;
namespace LibrarySystem.Application.Authentication.Customers;
public interface ICustomerRegisterationService
{
    Task<Result<bool>> Register(CustomerRegisterRequest customerRegisterRequest, CancellationToken? cancellationToken = null);
}