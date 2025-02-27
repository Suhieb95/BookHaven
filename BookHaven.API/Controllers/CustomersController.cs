using BookHaven.API.Filters;
using BookHaven.Application.Authentication.Customers;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs;
using BookHaven.Domain.DTOs.Auth;
using BookHaven.Domain.DTOs.Customers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
namespace BookHaven.API.Controllers;

[EnableRateLimiting("LoginLimiterPolicy")]
[AllowAnonymous]
public class CustomersController(ICustomerRegistrationService _customerRegisterationService, ICustomerPasswordResetService _customerPasswordResetService, ICustomerLoginService _customerLoginService, IRefreshTokenCookieSetter _refreshTokenCookieSetter, ICustomerUpdateService _updateCustomerService, IRefreshTokenValidator _IRefreshTokenValidator) : BaseController
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
    [Authorize]
    [EnableRateLimiting("StandardLimiterPolicy")]
    [HttpDelete(Person.RemoveProfilePicture)]
    public async Task<IActionResult> RemoveProfilePicture([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        Result<bool>? result = await _updateCustomerService.RemoveProfilePicture(id, cancellationToken);
        return result.Map(
           onSuccess: _ => NoContent(),
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
    [EnableRateLimiting("StandardLimiterPolicy")]
    [HttpPost(Person.RefreshToken)]
    [AllowAnonymous]
    public async Task<IActionResult> GenerateRefreshToken(CancellationToken cancellationToken)
    {
        string? token = HttpContext.Request.Cookies["refreshToken"];
        Result<RefreshToken>? result = await _IRefreshTokenValidator.ValidateRefreshToken(token, cancellationToken);

        return result.Map(
            onSuccess: _ => CreatedAtAction(nameof(GenerateRefreshToken), _),
            onFailure: Problem
            );
    }
}