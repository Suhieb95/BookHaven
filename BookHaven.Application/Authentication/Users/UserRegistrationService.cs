using BookHaven.Application.Authentication.Common;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs.Auth;
using BookHaven.Domain.DTOs.Users;
using BookHaven.Domain.Enums;
using BookHaven.Domain.Specification.Users;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace BookHaven.Application.Authentication.Users;
public class UserRegistrationService(IUnitOfWork _unitOfWork, IWebHostEnvironment _env, INotificationService _iNotificationService, IOptions<EmailSettings> emailSettings, IJwtTokenGenerator _jwtTokenGenerator) : IUserRegistrationService
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;
    public async Task<Result<bool>> ConfirmEmailAddress(Guid id, CancellationToken? cancellationToken = null)
    {
        User? user = (await _unitOfWork.Users.GetAll(new GetUserById(id), cancellationToken)).FirstOrDefault();
        if (user is null)
            return Result<bool>.Failure(new Error("User Doesn't Exist", NotFound, "User Not Found"));

        if (user is { IsActive: true })
            return Result<bool>.Failure(new Error("User with this Email Address Already Exists.", BadRequest, "User Exists"));

        if (!user.HasValidEmailConfirmationToken())
            return Result<bool>.Failure(new Error("Token Has Expired.", BadRequest, "Invalid Token"));

        await _unitOfWork.UserSecurity.UpdateEmailConfirmationToken(id, cancellationToken, UserType.Internal);
        await EmailHelpers.SendEmailConfirmedNotify(user!.EmailAddress, _emailSettings.SuccessURL, _env, cancellationToken, _iNotificationService);

        await GenerateAndSendSetPassowrdLink(user!.EmailAddress, user.Id, cancellationToken);
        return Result<bool>.Success(true);
    }
    public async Task<Result<bool>> Register(InternalUserRegisterRequest request, CancellationToken? cancellationToken = null)
    {
        User? user = (await _unitOfWork.Users.GetAll(new GetUserByEmailAddress(request.EmailAddress), cancellationToken)).FirstOrDefault();
        if (user is not null and { IsActive: true })
            return Result<bool>.Failure(new Error("User with this Email Address Already Exists.", BadRequest, "User Exists"));

        (bool flowControl, Result<bool>? result) = await CheckUserNameExistence.ValidateUserName<bool>(_unitOfWork, request.UserName, userType: UserType.Internal, cancellationToken: cancellationToken);
        if (flowControl == false) return result!;

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
        await _unitOfWork.UserSecurity.SaveEmailConfirmationToken(emailConfirmationResult, cancellationToken, UserType.Internal);
        await EmailHelpers.SendConfirmationEmail(user!.EmailAddress, id, _emailSettings.EmailConfirmationURL, _env, cancellationToken, _iNotificationService);
    }
    private async Task GenerateAndSendSetPassowrdLink(string to, Guid id, CancellationToken? cancellationToken)
    {
        ResetPasswordResult? resetPasswordResult = _jwtTokenGenerator.GeneratePasswordResetToken(to);
        await _unitOfWork.UserSecurity.SavePassowordResetToken(resetPasswordResult, cancellationToken, UserType.Internal);
        await EmailHelpers.SendResetPasswordLink(to, id, _emailSettings.ResetPasswordURL, _env, cancellationToken, _iNotificationService);
    }
}