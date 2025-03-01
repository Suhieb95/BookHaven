using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs;
using BookHaven.Domain.DTOs.Auth;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BookHaven.Application.Authentication.Common;
internal static class EmailHelpers
{
    internal static async Task SendConfirmationEmail(string to, Guid id, string emailConfirmationURL, IWebHostEnvironment _env, CancellationToken? cancellationToken, INotificationService _iNotificationService)
    {
        string? filePath = Path.Combine(_env.WebRootPath, "Templates", "EmailConfirmation.html");
        StreamReader? str = new(filePath);
        string? mailText = await str.ReadToEndAsync();
        str.Close();

        mailText = mailText.Replace("[url]", emailConfirmationURL + id.ToString())
                           .Replace("[year]", DateTime.Today.Year.ToString());

        EmailRequest email = new(to, "Complete Registration", mailText);
        await _iNotificationService.SendEmail(email, cancellationToken);
    }
    internal static async Task SendEmailConfirmedNotify(string to, string successURL, IWebHostEnvironment _env, CancellationToken? cancellationToken, INotificationService _iNotificationService)
    {
        string? filePath = Path.Combine(_env.WebRootPath, "Templates", "SuccessTemplate.html");
        StreamReader? str = new(filePath);
        string? mailText = await str.ReadToEndAsync();
        str.Close();

        mailText = mailText.Replace("[msg]", $"Your Email Address Has been Successfully Verified.")
                           .Replace("[url]", successURL)
                           .Replace("[title]", "Email Confirmation")
                           .Replace("[header]", "Email Confirmation")
                           .Replace("[year]", DateTime.Today.Year.ToString());

        EmailRequest email = new(to, "Email Confirmation", mailText);
        await _iNotificationService.SendEmail(email, cancellationToken);
    }
    internal static async Task SendResetPasswordLink(string to, Guid id, string resetPasswordURL, IWebHostEnvironment _env, CancellationToken? cancellationToken, INotificationService _iNotificationService)
    {
        string path = Path.Combine(_env.WebRootPath, "Templates", "PasswordTemplate.html");
        StreamReader? str = new(path);
        string content = await str.ReadToEndAsync();
        str.Close();

        content = content.Replace("[url]", resetPasswordURL + id.ToString())
                         .Replace("[year]", DateTime.Now.Year.ToString());

        EmailRequest email = new(to, "Password Reset", content);
        await _iNotificationService.SendEmail(email, cancellationToken);
    }
    internal static async Task SendPasswordChangedNotify(string to, string successURL, IWebHostEnvironment _env, CancellationToken? cancellationToken, INotificationService _iNotificationService)
    {
        string templatePath = Path.Combine(_env.WebRootPath, "Templates", "SuccessTemplate.html");
        StreamReader str = new(templatePath);
        string content = await str.ReadToEndAsync();
        str.Close();

        content = content.Replace("[msg]", $"Your Password Has been Successfully Changed.")
                                  .Replace("[url]", successURL)
                                  .Replace("[title]", "Password Changed")
                                  .Replace("[header]", "Password Changed")
                                  .Replace("[year]", DateTime.Today.Year.ToString());

        EmailRequest email = new(to, "Password Changed", content);
        await _iNotificationService.SendEmail(email, cancellationToken);
    }
    internal static async Task SendNotifyLoginEmail(string to, string successURL, IDateTimeProvider _dateTimeProvider, IWebHostEnvironment _env, CancellationToken? cancellationToken, INotificationService _iNotificationService, IHttpContextAccessor _httpContextAccessor, IIPApiClient _IPApiClient)
    {
        string? filePath = Path.Combine(_env.WebRootPath, "Templates", "SuccessTemplate.html");
        StreamReader? str = new(filePath);
        string? mailText = str.ReadToEnd();
        str.Close();

        string? ipAddress = await GetIpAddressDetails(cancellationToken, _httpContextAccessor, _IPApiClient);

        mailText = mailText.Replace("[msg]", $"Your Were successfully logged int at {_dateTimeProvider.UtcNow.AddHours(4)} GMT +4, From {ipAddress}.")
                           .Replace("[title]", "Login Successful")
                           .Replace("[url]", successURL)
                           .Replace("[header]", "Login Activity")
                           .Replace("[year]", DateTime.Today.Year.ToString());

        EmailRequest email = new(to, "Login Activity", mailText);
        await _iNotificationService.SendEmail(email);
    }
    private static async Task<string?> GetIpAddressDetails(CancellationToken? cancellationToken, IHttpContextAccessor _httpContextAccessor, IIPApiClient _IPApiClient)
    {
        string? ipAddress = _httpContextAccessor.HttpContext?.GetServerVariable("HTTP_X_FORWARDED_FOR") ?? _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        string? ipAddressWithoutPort = ipAddress?.Split(':')[0];
        IPApiResponse? ipApiResponse = await _IPApiClient.Get(ipAddressWithoutPort, cancellationToken ?? CancellationToken.None);
        return $"{ipApiResponse?.Country} - {ipApiResponse?.City}";
    }
}