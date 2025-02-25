using LibrarySystem.Application.Authentication.Common;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs.Auth;
using LibrarySystem.Domain.DTOs.Users;
using LibrarySystem.Domain.Specification.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace LibrarySystem.Application.Authentication.Users;
public class UserRegistrationService(IUnitOfWork _unitOfWork, IWebHostEnvironment _env, INotificationService _iNotificationService, IOptions<EmailSettings> emailSettings, IJwtTokenGenerator _jwtTokenGenerator) : IUserRegistrationService
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;
    public async Task<Result<bool>> ConfirmEmailAddress(Guid id, CancellationToken? cancellationToken = null)
    {
        User? user = (await _unitOfWork.Users.GetAll(new GetUserById(id), cancellationToken)).FirstOrDefault();
        if (user is null)
            return Result<bool>.Failure(new("User Doesn't Exist", NotFound, "User Not Found"));

        if (user is { IsActive: true })
            return Result<bool>.Failure(new("User with this Email Address Already Exists.", BadRequest, "User Exists"));

        if (!user.HasValidEmailConfirmationToken())
            return Result<bool>.Failure(new("Token Has Expired.", BadRequest, "Invalid Token"));

        await _unitOfWork.Users.UpdateEmailConfirmationToken(id, cancellationToken);
        await EmailHelpers.SendEmailConfirmedNotify(user!.EmailAddress, _emailSettings.SuccessURL, _env, cancellationToken, _iNotificationService);

        await GenerateAndSendSetPassowrdLink(user!.EmailAddress, user.Id, cancellationToken);
        return Result<bool>.Success(true);
    }
    public async Task<Result<bool>> Register(InternalUserRegisterRequest request, CancellationToken? cancellationToken = null)
    {
        User? user = (await _unitOfWork.Users.GetAll(new GetUserByEmailAddress(request.EmailAddress), cancellationToken)).FirstOrDefault();
        if (user is not null and { IsActive: true })
            return Result<bool>.Failure(new("User with this Email Address Already Exists.", BadRequest, "User Exists"));

        Guid id = await _unitOfWork.Users.Add(request, cancellationToken);
        await SendEmailConfirmationLink(cancellationToken, id);
        return Result<bool>.Success(true);
    }
    private async Task SendEmailConfirmationLink(CancellationToken? cancellationToken, Guid id)
    {
        User? user = (await _unitOfWork.Users.GetAll(new GetUserById(id), cancellationToken)).FirstOrDefault();
        if (user is not null && user.HasValidEmailConfirmationToken())
            return;

        EmailConfirmationResult emailConfirmationResult = _jwtTokenGenerator.GenerateEmailConfirmationToken(id);
        await _unitOfWork.Users.SaveEmailConfirmationToken(emailConfirmationResult, cancellationToken);
        await EmailHelpers.SendConfirmationEmail(user!.EmailAddress, id, _emailSettings.EmailConfirmationURL, _env, cancellationToken, _iNotificationService);
    }
    private async Task GenerateAndSendSetPassowrdLink(string to, Guid id, CancellationToken? cancellationToken)
    {
        ResetPasswordResult? resetPasswordResult = _jwtTokenGenerator.GeneratePasswordResetToken(to);
        await _unitOfWork.Users.SavePassowordResetToken(resetPasswordResult, cancellationToken);
        await EmailHelpers.SendResetPasswordLink(to, id, _emailSettings.ResetPasswordURL, _env, cancellationToken, _iNotificationService);
    }
}