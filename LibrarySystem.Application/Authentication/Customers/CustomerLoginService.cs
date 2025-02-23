using LibrarySystem.Application.Interfaces;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.DTOs.Auth;
using LibrarySystem.Domain.DTOs.Customers;
using LibrarySystem.Domain.Specification.Customers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace LibrarySystem.Application.Authentication.Customers;
public class CustomerLoginService(IUnitOfWork _iUnitOfWork, IJwtTokenGenerator _jwtTokenGenerator, INotificationService _iNotificationService, IIPApiClient _IPApiClient
    , IWebHostEnvironment _env, IOptions<EmailSettings> emailSettings, IPasswordHasher _passwordHasher, IFileService _fileService, IDateTimeProvider _dateTimeProvider, IHttpContextAccessor _httpContextAccessor)
        : ICustomerLoginService
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;
    public async Task<Result<CustomerLoginResponse>> Login(CustomerLoginRequest request, CancellationToken? cancellationToken)
    {
        Customer? currentUser = await GetUser(request.EmailAddress, cancellationToken);
        if (currentUser is { IsActive: false })
            return Result<CustomerLoginResponse>.Failure(new("Invalid Email Address Or Password", BadRequest, "Incorrect Credentials"));

        if (IsIncorrectPassowrd(request.Password, currentUser!.Password))
            return Result<CustomerLoginResponse>.Failure(new("Invalid Email Address Or Password", BadRequest, "Incorrect Credentials"));

        string token = await _jwtTokenGenerator.GenerateAccessToken(currentUser);
        string? imageUrl = await FetchUserImage(currentUser.ImageUrl);
        await SendNotifyLoginEmail(request.EmailAddress, cancellationToken);
        return Result<CustomerLoginResponse>.Success(new(currentUser.EmailAddress, currentUser.UserName, imageUrl ?? string.Empty, token, currentUser.Id));
    }
    private async Task<string?> FetchUserImage(string? publicId)
    {
        if (string.IsNullOrEmpty(publicId)) return null;
        return await _fileService.GetFile(publicId);
    }
    private bool IsIncorrectPassowrd(string password, string hash) => !_passwordHasher.VerifyPassword(password, hash);
    private async Task<Customer?> GetUser(string emailAddress, CancellationToken? cancellationToken)
            => (await _iUnitOfWork.Customers.GetAll(new GetCustomerByEmailAddress(emailAddress), cancellationToken)).FirstOrDefault();
    private async Task SendNotifyLoginEmail(string to, CancellationToken? cancellationToken)
    {
        string? filePath = Path.Combine(_env.WebRootPath, "Templates", "SuccessTemplate.html");
        StreamReader? str = new(filePath);
        string? mailText = str.ReadToEnd();
        str.Close();

        string? ipAddress = await GetIpAddressDetails(cancellationToken);

        mailText = mailText.Replace("[msg]", $"Your Were successfully logged int at {_dateTimeProvider.UtcNow.AddHours(4)} GMT +4, From {ipAddress}.")
                           .Replace("[title]", "Login Successful")
                           .Replace("[url]", _emailSettings.SuccessURL)
                           .Replace("[header]", "Login Activity")
                           .Replace("[year]", DateTime.Today.Year.ToString());

        EmailRequest email = new(to, "Login Activity", mailText);
        await _iNotificationService.SendEmail(email);
    }
    private async Task<string?> GetIpAddressDetails(CancellationToken? cancellationToken)
    {
        string? ipAddress = _httpContextAccessor.HttpContext?.GetServerVariable("HTTP_X_FORWARDED_FOR") ?? _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        string? ipAddressWithoutPort = ipAddress?.Split(':')[0];
        IpApiResponse? ipApiResponse = await _IPApiClient.Get(ipAddressWithoutPort, cancellationToken ?? CancellationToken.None);
        return $"{ipApiResponse?.Country} - {ipApiResponse?.City}";
    }
}