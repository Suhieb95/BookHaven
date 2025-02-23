using LibrarySystem.Domain.DTOs.Customers;
namespace LibrarySystem.Application.Authentication.Customers;
public interface ICustomerLoginService
{
    Task<Result<CustomerLoginResponse>> Login(CustomerLoginRequest request, CancellationToken? cancellationToken);
}