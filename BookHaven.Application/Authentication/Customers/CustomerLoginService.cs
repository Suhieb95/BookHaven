using BookHaven.Application.Authentication.Common;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs.Customers;
using BookHaven.Domain.Specification.Customers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BookHaven.Application.Authentication.Customers;
public class CustomerLoginService(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator, INotificationService notificationService, IIPApiClient IPApiClient
    , IWebHostEnvironment env, IOptions<EmailSettings> emailSettings, IPasswordHasher passwordHasher, IFileService fileService, IDateTimeProvider dateTimeProvider, IHttpContextAccessor httpContextAccessor)
        : ICustomerLoginService
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

    public async Task<Result<CustomerLoginResponse>> Login(CustomerLoginRequest request, CancellationToken? cancellationToken = default)
    {
        Customer? currentUser = await GetCustomer(request.EmailAddress, cancellationToken);
        if (currentUser is null || currentUser is not null and { IsActive: false })
            return Result<CustomerLoginResponse>.Failure(new("Invalid Email Address Or Password", BadRequest, "Incorrect Credentials"));

        if (IsIncorrectPassowrd(request.Password, currentUser!.Password))
            return Result<CustomerLoginResponse>.Failure(new("Invalid Email Address Or Password", BadRequest, "Incorrect Credentials"));

        string token = _jwtTokenGenerator.GenerateAccessToken(currentUser);
        string refreshToken = _jwtTokenGenerator.GenerateRefreshToken(currentUser);
        string? imageUrl = await FetchUserImage(currentUser.ImageUrl, cancellationToken);
        await EmailHelpers.SendNotifyLoginEmail(currentUser.EmailAddress, _emailSettings.SuccessURL, _dateTimeProvider, _env, cancellationToken, _notificationService, _httpContextAccessor, _iPApiClient);
        return Result<CustomerLoginResponse>.Success(new(currentUser.EmailAddress, currentUser.UserName, imageUrl ?? string.Empty, "Bearer " + token, currentUser.Id, refreshToken));
    }
    private async Task<string?> FetchUserImage(string? publicId, CancellationToken? cancellationToken)
    {
        if (string.IsNullOrEmpty(publicId)) return null;
        return await _fileService.GetFile(publicId, cancellationToken);
    }
    private bool IsIncorrectPassowrd(string password, string hash) => !_passwordHasher.VerifyPassword(password, hash);
    private async Task<Customer?> GetCustomer(string emailAddress, CancellationToken? cancellationToken)
            => (await _unitOfWork.Customers.GetAll(new GetCustomerByEmailAddress(emailAddress), cancellationToken)).FirstOrDefault();
}