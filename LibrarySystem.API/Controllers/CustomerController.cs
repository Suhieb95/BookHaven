using LibrarySystem.Application.Authentication.Customers;
using LibrarySystem.Domain.DTOs.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.API.Controllers;
[AllowAnonymous]
public class CustomerController(ICustomerRegistrationService _customerRegisterationService) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add(CustomerRegisterRequest request)
    {
        var result = await _customerRegisterationService.Register(request);
        return result.Map(
            onSuccess: _ => Ok(new { Message = "Please Check your inbox." }),
            onFailure: Problem
            );
    }
}
