using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs.Customers;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Specification.Customers;

namespace LibrarySystem.Application.Authentication.Customers;
public class CustomerRegisterationService(ICustomerService _customerService) : ICustomerRegisterationService
{
    public async Task<Result<CustomerRegisterResponse>> Register(CustomerRegisterRequest customerRegisterRequest, CancellationToken? cancellationToken = null)
    {
        if (await IsEmailAddressInUse(customerRegisterRequest.EmailAddress, cancellationToken))
            return Result<CustomerRegisterResponse>.Failure(new Error("Email Address exists, Please Login.", BadRequest, "Email Address Exists"));

        return Result<CustomerRegisterResponse>.Success(new CustomerRegisterResponse("", "", "", "", new Guid()));
    }
    private async Task<bool> IsEmailAddressInUse(string emailAddress, CancellationToken? cancellationToken)
    {
        var res = await _customerService.GetAll(new GetCustomerByEmailAddress(emailAddress), cancellationToken);
        if (res.FirstOrDefault() != null)
            return true;

        return false;
    }
}
