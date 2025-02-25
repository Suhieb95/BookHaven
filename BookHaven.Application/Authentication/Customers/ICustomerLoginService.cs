using BookHaven.Domain.DTOs.Customers;
namespace BookHaven.Application.Authentication.Customers;
public interface ICustomerLoginService
{
    Task<Result<CustomerLoginResponse>> Login(CustomerLoginRequest request, CancellationToken? cancellationToken = default);
}