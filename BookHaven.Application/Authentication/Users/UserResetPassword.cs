using BookHaven.Application.Authentication.Common;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs.Auth;
using BookHaven.Domain.Enums;
using BookHaven.Domain.Specification;
using BookHaven.Domain.Specification.Users;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace BookHaven.Application.Authentication.Users;
public class UserResetPassword(IUnitOfWork unitOfWork, IOptions<EmailSettings> emailSettings, IPasswordHasher passwordHasher, INotificationService notificationService, IWebHostEnvironment env, IJwtTokenGenerator jwtTokenGenerator) : IUserResetPassword
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly INotificationService _notificationService = notificationService;
    private readonly IWebHostEnvironment _env = env;
    public async Task<Result<bool>> ChangePassword(PasswordChangeRequest request, CancellationToken? cancellationToken = null)
    {
        Result<bool> result = await VerifyToken(request.Id, cancellationToken);
        if (!result.IsSuccess)
            return result;

        request.SetPassword(_passwordHasher.Hash(request.Password));
        await _unitOfWork.UserSecurity.UpdatePassowordResetToken(request, cancellationToken, UserType.Customer);
        User? user = await GetUser(new GetUserById(request.Id), cancellationToken);
        await EmailHelpers.SendPasswordChangedNotify(user!.EmailAddress, _emailSettings.ResetPasswordURL, _env, cancellationToken, _notificationService);

        return Result<bool>.Success(true);
    }
    public async Task<Result<bool>> ResetPassword(string emailAddress, CancellationToken? cancellationToken = null)
    {
        User? user = await GetUser(new GetUserByEmailAddress(emailAddress), cancellationToken);
        if (user is null || user is not null and { IsVerified: false })
            return Result<bool>.Failure(new("User Doesn't With this Exists.", BadRequest, "Invalid User"));

        ResetPasswordResult resetPassword = _jwtTokenGenerator.GeneratePasswordResetToken(emailAddress);
        await _unitOfWork.UserSecurity.SavePassowordResetToken(resetPassword, cancellationToken, UserType.Customer);
        await EmailHelpers.SendResetPasswordLink(user!.EmailAddress, user.Id, _emailSettings.ResetPasswordURL, _env, cancellationToken, _notificationService);
        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> VerifyToken(Guid id, CancellationToken? cancellationToken = null)
    {
        User? user = await GetUser(new GetUserById(id), cancellationToken);
        if (user is null || user is not null and { IsVerified: false })
            return Result<bool>.Failure(new Error("User Doesn't Exists.", BadRequest, "Invalid User"));

        if (!user!.HasValidRestPasswordToken())
            return Result<bool>.Failure(new Error("Token Has Expired, Please request password change again.", BadRequest, "Invalid Token"));

        return Result<bool>.Success(true);
    }
    private async Task<T?> GetUser<T>(Specification<T> specification, CancellationToken? cancellationToken)
       => await _unitOfWork.Users.GetBy(specification, cancellationToken);
}