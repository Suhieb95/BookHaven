using LibrarySystem.API.Filters;
using LibrarySystem.Application.Authentication.Customers;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs.Auth;
using LibrarySystem.Domain.DTOs.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
namespace LibrarySystem.API.Controllers;

[EnableRateLimiting("LoginLimiterPolicy")]
[AllowAnonymous]
public class CustomerController(ICustomerRegistrationService _customerRegisterationService, ICustomerPasswordResetService _customerPasswordResetService, ICustomerLoginService _customerLoginService, IRefreshTokenCookieSetter _refreshTokenCookieSetter, ICustomerUpdateService _updateCustomerService) : BaseController
{
    [HttpPost(Person.Register)]
    public async Task<IActionResult> Add(CustomerRegisterRequest request, CancellationToken cancellationToken)
    {
        Result<bool>? result = await _customerRegisterationService.Register(request, cancellationToken);
        return result.Map(
            onSuccess: _ => Ok(new { Message = "Please Check your inbox." }),
            onFailure: Problem
            );
    }
    [EnableRateLimiting("StandardLimiterPolicy")]
    [HttpPut]
    public async Task<IActionResult> Update([FromForm] CustomerUpdateRequest request, CancellationToken cancellationToken)
    {
        Result<bool>? result = await _updateCustomerService.Update(request, cancellationToken);
        return result.Map(
            onSuccess: _ => NoContent(),
            onFailure: Problem
            );
    }
    [ServiceFilter(typeof(LastLoginFilter))]
    [HttpPost(Person.Login)]
    public async Task<IActionResult> Login(CustomerLoginRequest request, CancellationToken cancellationToken)
    {
        Result<CustomerLoginResponse>? result = await _customerLoginService.Login(request, cancellationToken);
        if (result.IsSuccess)
            _refreshTokenCookieSetter.SetJwtTokenCookie(HttpContext, result.Data!.Token);

        return result.Map(
            onSuccess: Ok,
            onFailure: Problem
            );
    }
    [HttpPut(Person.ChangePassword)]
    public async Task<IActionResult> ChangePassword(PasswordChangeRequest passwordChangeRequest, CancellationToken cancellationToken)
    {
        Result<bool>? result = await _customerPasswordResetService.ChangePassword(passwordChangeRequest, cancellationToken);
        return result.Map(
            onSuccess: _ => NoContent(),
            onFailure: Problem
            );
    }
    [HttpPost(Person.ResetPasswordRequest)]
    public async Task<IActionResult> ResetPasswordRequest([FromQuery] string emailAddress, CancellationToken cancellationToken)
    {
        Result<bool>? result = await _customerPasswordResetService.ResetPassword(emailAddress, cancellationToken);
        return result.Map(
            onSuccess: _ => Ok(new { Message = "Please Check your inbox." }),
            onFailure: Problem
            );
    }
    [HttpGet(Person.VerifyResetPasswordToken)]
    public async Task<IActionResult> VerifyResetPasswordToken([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        Result<bool>? result = await _customerPasswordResetService.VerifyToken(id, cancellationToken);
        return result.Map(
            onSuccess: _ => Ok(new { Message = "You may Reset Your password Now." }),
            onFailure: Problem
            );
    }
    [HttpPost(Person.ConfirmEmailAddress)]
    public async Task<IActionResult> ConfirmEmail(Guid id, CancellationToken cancellationToken)
    {
        Result<bool>? result = await _customerRegisterationService.ConfirmEmailAddress(id, cancellationToken);
        return result.Map(
            onSuccess: _ => Ok(new { Message = "Email Address Confirmed, you may Login now." }),
            onFailure: Problem
            );
    }
    [HttpPost(Person.Logout)]
    [EnableRateLimiting("StandardLimiterPolicy")]
    public IActionResult Logout()
    {
        _refreshTokenCookieSetter.DeleteJwtTokenCookie(HttpContext, "refreshToken");
        return NoContent();
    }
}
