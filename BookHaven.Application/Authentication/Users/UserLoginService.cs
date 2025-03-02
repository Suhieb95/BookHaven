using BookHaven.Application.Authentication.Common;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs.Users;
using BookHaven.Domain.Enums;
using BookHaven.Domain.Specification.Users;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BookHaven.Application.Authentication.Users;
public class UserLoginService(IUnitOfWork unitOfWork, IOptions<EmailSettings> emailSettings, IPasswordHasher passwordHasher, INotificationService notificationService, IWebHostEnvironment env, IJwtTokenGenerator jwtTokenGenerator, IFileService fileService, IIPApiClient IPApiClient, IDateTimeProvider dateTimeProvider, IHttpContextAccessor httpContextAccessor)
    : IUserLoginService
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly INotificationService _notificationService = notificationService;
    private readonly IIPApiClient _iPApiClient = IPApiClient;
    private readonly IWebHostEnvironment _env = env;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IFileService _fileService = fileService;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    public async Task<Result<InternalUserLoginResponse>> Login(InternalUserLoginRequest request, CancellationToken? cancellationToken = default)
    {
        User? currentUser = (await _unitOfWork.Users.GetAll(new GetUserByEmailAddress(request.EmailAddress), cancellationToken)).FirstOrDefault();
        if (currentUser is null || currentUser is not null and { IsActive: false })
            return Result<InternalUserLoginResponse>.Failure(new("Invalid Email Address Or Password", BadRequest, "Incorrect Credentials"));

        if (IsIncorrectPassowrd(request.Password, currentUser!.Password))
            return Result<InternalUserLoginResponse>.Failure(new("Invalid Email Address Or Password", BadRequest, "Incorrect Credentials"));

        string? imageUrl = await FetchUserImage(currentUser.ImageUrl, cancellationToken);
        (string[] roles, string[] permissions) = await AddUserPermissionsAndRoles(currentUser.Id, cancellationToken);
        string token = _jwtTokenGenerator.GenerateAccessToken(currentUser, UserType.Internal, roles, permissions);
        string refreshToken = _jwtTokenGenerator.GenerateRefreshToken(currentUser, UserType.Internal);
        await EmailHelpers.SendNotifyLoginEmail(currentUser.EmailAddress, _emailSettings.SuccessURL, _dateTimeProvider, _env, cancellationToken, _notificationService, _httpContextAccessor, _iPApiClient);

        InternalUserLoginResponse response = new(
            currentUser.EmailAddress,
            currentUser.UserName,
            "Bearer " + token,
            currentUser.Id,
            refreshToken,
            imageUrl ?? string.Empty);

        return Result<InternalUserLoginResponse>.Success(response);
    }
    private async Task<string?> FetchUserImage(string? publicId, CancellationToken? cancellationToken)
        => string.IsNullOrEmpty(publicId) ? null : await _fileService.GetFile(publicId, cancellationToken);
    private async Task<(string[] roles, string[] permissions)> AddUserPermissionsAndRoles(Guid id, CancellationToken? cancellationToken = default)
    {
        string[] roles = await _unitOfWork.Users.GetUserRoles(id, cancellationToken);
        string[] permissions = await _unitOfWork.Users.GetUserPermissions(id, cancellationToken);
        return (roles, permissions);
    }
    private bool IsIncorrectPassowrd(string password, string hash) => !_passwordHasher.VerifyPassword(password, hash);
}