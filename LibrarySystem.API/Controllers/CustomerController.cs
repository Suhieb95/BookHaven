using LibrarySystem.Application.Authentication.Customers;
using LibrarySystem.Domain.DTOs.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.API.Controllers;
[AllowAnonymous]
public class CustomerController(ICustomerRegisterationService _customerRegisterationService) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Get(CustomerRegisterRequest registerRequest, CancellationToken cancellationToken)
    {
        var result = await _customerRegisterationService.Register(registerRequest, cancellationToken);
        return result.Map(
            onSuccess: Ok,
            onFailure: Problem
            );
    }
}
