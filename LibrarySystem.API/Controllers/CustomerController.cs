using LibrarySystem.Application.Authentication.Customers;
using LibrarySystem.Domain.DTOs.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.API.Controllers;
[AllowAnonymous]
public class CustomerController(ICustomerRegistrationService _customerRegisterationService) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add(CustomerRegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await _customerRegisterationService.Register(request, cancellationToken);
        return result.Map(
            onSuccess: _ => Ok(new { Message = "Please Check your inbox." }),
            onFailure: Problem
            );
    }
    [HttpPost(Customers.ConfirmEmailAddress)]
    public async Task<IActionResult> ConfirmEmail(Guid id, CancellationToken cancellationToken)
    {
        var result = await _customerRegisterationService.ConfirmEmailAddress(id, cancellationToken);
        return result.Map(
            onSuccess: _ => Ok(new { Message = "Email Address Confirmed, you may Login now." }),
            onFailure: Problem
            );
    }
}
