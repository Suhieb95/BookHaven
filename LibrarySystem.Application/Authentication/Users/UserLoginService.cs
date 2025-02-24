using LibrarySystem.Application.Authentication.Common;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs.Users;
using LibrarySystem.Domain.Enums;
using LibrarySystem.Domain.Specification.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace LibrarySystem.Application.Authentication.Users;
public class UserLoginService(IUnitOfWork _unitOfWork, IOptions<EmailSettings> emailSettings, IPasswordHasher _passwordHasher, INotificationService _notificationService, IWebHostEnvironment _env, IJwtTokenGenerator _jwtTokenGenerator, IFileService _fileService, IIPApiClient _IPApiClient, IDateTimeProvider _dateTimeProvider, IHttpContextAccessor _httpContextAccessor)
    : IUserLoginService
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;
    public async Task<Result<InternalUserLoginResponse>> Login(InternalUserLoginRequest request, CancellationToken? cancellationToken = null)
    {
        User? currentUser = (await _unitOfWork.Users.GetAll(new GetUserByEmailAddress(request.EmailAddress), cancellationToken)).FirstOrDefault();
        if (currentUser is null || currentUser is not null and { IsActive: false })
            return Result<InternalUserLoginResponse>.Failure(new("Invalid Email Address Or Password", BadRequest, "Incorrect Credentials"));

        if (IsIncorrectPassowrd(request.Password, currentUser!.Password))
            return Result<InternalUserLoginResponse>.Failure(new("Invalid Email Address Or Password", BadRequest, "Incorrect Credentials"));

        string? imageUrl = await FetchUserImage(currentUser.ImageUrl);
        (string[] roles, string[] permissions) = await AddUserPermissionsAndRoles(currentUser.Id, cancellationToken);
        string token = _jwtTokenGenerator.GenerateAccessToken(currentUser, PersonType.InternalUser, roles, permissions);
        await EmailHelpers.SendNotifyLoginEmail(currentUser.EmailAddress, _emailSettings.SuccessURL, _dateTimeProvider, _env, cancellationToken, _notificationService, _httpContextAccessor, _IPApiClient);

        InternalUserLoginResponse response = new(
            currentUser.EmailAddress,
            currentUser.UserName,
            imageUrl ?? string.Empty,
            "Bearer " + token,
            currentUser.Id);

        return Result<InternalUserLoginResponse>.Success(response);
    }
    private async Task<string?> FetchUserImage(string? publicId)
    {
        if (string.IsNullOrEmpty(publicId)) return null;
        return await _fileService.GetFile(publicId);
    }
    private async Task<(string[] roles, string[] permissions)> AddUserPermissionsAndRoles(Guid id, CancellationToken? cancellationToken = default)
    {
        string[] roles = await _unitOfWork.Users.GetUserRoles(id, cancellationToken);
        string[] permissions = await _unitOfWork.Users.GetUserPermissions(id, cancellationToken);
        return (roles, permissions);
    }
    private bool IsIncorrectPassowrd(string password, string hash) => !_passwordHasher.VerifyPassword(password, hash);
}
