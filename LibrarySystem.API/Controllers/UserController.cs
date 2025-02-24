using LibrarySystem.API.Filters;
using LibrarySystem.Application.Authentication.Users;
using LibrarySystem.Domain.DTOs.Auth;
using LibrarySystem.Domain.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace LibrarySystem.API.Controllers;

[EnableRateLimiting("LoginLimiterPolicy")]
[AllowAnonymous]
public class UserController(IUserRegistrationService _userRegistrationService, IUserResetPassword _userResetPassword, IUserLoginService _userLoginService) : BaseController
{
    [HttpPost(Person.Register)]
    public async Task<IActionResult> Add(InternalUserRegisterRequest request, CancellationToken cancellationToken)
    {
        Result<bool>? result = await _userRegistrationService.Register(request, cancellationToken);
        return result.Map(
            onSuccess: _ => Ok(new { Message = "Please Check your inbox." }),
            onFailure: Problem
            );
    }
    [ServiceFilter(typeof(LastLoginFilter))]
    [HttpPost(Person.Login)]
    public async Task<IActionResult> Login(InternalUserLoginRequest request, CancellationToken cancellationToken)
    {
        Result<InternalUserLoginResponse>? result = await _userLoginService.Login(request, cancellationToken);
        return result.Map(
            onSuccess: Ok,
            onFailure: Problem
            );
    }
    [HttpPut(Person.ChangePassword)]
    public async Task<IActionResult> ChangePassword(PasswordChangeRequest passwordChangeRequest, CancellationToken cancellationToken)
    {
        Result<bool>? result = await _userResetPassword.ChangePassword(passwordChangeRequest, cancellationToken);
        return result.Map(
            onSuccess: _ => NoContent(),
            onFailure: Problem
            );
    }
    [HttpPost(Person.ResetPasswordRequest)]
    public async Task<IActionResult> ResetPasswordRequest([FromQuery] string emailAddress, CancellationToken cancellationToken)
    {
        Result<bool>? result = await _userResetPassword.ResetPassword(emailAddress, cancellationToken);
        return result.Map(
            onSuccess: _ => Ok(new { Message = "Please Check your inbox." }),
            onFailure: Problem
            );
    }
    [HttpGet(Person.VerifyResetPasswordToken)]
    public async Task<IActionResult> VerifyResetPasswordToken([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        Result<bool>? result = await _userResetPassword.VerifyToken(id, cancellationToken);
        return result.Map(
            onSuccess: _ => Ok(new { Message = "You may Reset Your password Now." }),
            onFailure: Problem
            );
    }
    [HttpPost(Person.ConfirmEmailAddress)]
    public async Task<IActionResult> ConfirmEmail(Guid id, CancellationToken cancellationToken)
    {
        Result<bool>? result = await _userRegistrationService.ConfirmEmailAddress(id, cancellationToken);
        return result.Map(
            onSuccess: _ => Ok(new { Message = "Email Address Confirmed, you may set your password now." }),
            onFailure: Problem
            );
    }
}