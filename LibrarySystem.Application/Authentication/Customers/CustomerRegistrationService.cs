using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.DTOs.Auth;
using LibrarySystem.Domain.DTOs.Customers;
using LibrarySystem.Domain.Specification.Customers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace LibrarySystem.Application.Authentication.Customers;
public class CustomerRegistrationService(IUnitOfWork _iUnitOfWork, IJwtTokenGenerator _jwtTokenGenerator, INotificationService _iNotificationService
    , IOptions<EmailSettings> emailSettings, IWebHostEnvironment _env, IPasswordHasher _passwordHasher) : ICustomerRegistrationService
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;
    public async Task<Result<bool>> Register(CustomerRegisterRequest request, CancellationToken? cancellationToken = null)
    {
        if (await IsEmailAddressInUse(request.EmailAddress, cancellationToken))
            return Result<bool>.Failure(new Error("Email Address exists, Please Login.", BadRequest, "Email Address Exists"));

        HashPassword(request);
        Guid result = await _iUnitOfWork.Customers.Add(request, cancellationToken);
        await SendEmailConfirmationToken(request.EmailAddress, result, cancellationToken);

        return Result<bool>.Success(true);
    }
    public async Task<Result<bool>> ConfirmEmailAddress(Guid id, CancellationToken? cancellationToken = null)
    {
        List<Customer>? res = await _iUnitOfWork.Customers.GetAll(new GetCustomerById(id), cancellationToken);
        Customer? currentUser = res.FirstOrDefault();
        if (currentUser is null)
            return Result<bool>.Failure(new Error("User Doesn't Exists.", BadRequest, "Invalid User"));

        if (!currentUser.HasValidEmailConfirmationToken())
            return Result<bool>.Failure(new Error("Token Has Expired.", BadRequest, "Invalid Token"));

        await _iUnitOfWork.Customers.UpdateEmailConfirmationToken(id, cancellationToken);
        await SendNotifyEmail(currentUser.EmailAddress, cancellationToken);
        return Result<bool>.Success(true);
    }
    private void HashPassword(CustomerRegisterRequest request)
       => request.SetPassword(_passwordHasher.Hash(request.Password));
    private async Task SendEmailConfirmationToken(string emailAddress, Guid result, CancellationToken? cancellationToken)
    {
        List<Customer>? res = await _iUnitOfWork.Customers.GetAll(new GetCustomerByEmailAddress(emailAddress), cancellationToken);
        Customer? currentUser = res.FirstOrDefault();
        if (currentUser != null && currentUser.HasValidEmailConfirmationToken())
            return;

        EmailConfirmationResult emailConfirmation = _jwtTokenGenerator.GenerateEmailConfirmationToken(result);
        await _iUnitOfWork.Customers.SaveEmailConfirmationToken(emailConfirmation, cancellationToken);
        await SendConfirmationEmail(emailAddress, result, cancellationToken);
    }
    private async Task<bool> IsEmailAddressInUse(string emailAddress, CancellationToken? cancellationToken)
    {
        List<Customer> res = await _iUnitOfWork.Customers.GetAll(new GetCustomerByEmailAddress(emailAddress), cancellationToken);
        Customer? currentUser = res.FirstOrDefault();
        if (currentUser is { IsActive: true })
            return true;

        return false;
    }
    private async Task SendNotifyEmail(string to, CancellationToken? cancellationToken)
    {
        string? filePath = Path.Combine(_env.WebRootPath, "Templates", "SuccessTemplate.html");
        StreamReader? str = new(filePath);
        string? mailText = await str.ReadToEndAsync();
        str.Close();

        mailText = mailText.Replace("[msg]", $"Your Email Address Has been Successfully Verified.")
                           .Replace("[url]", _emailSettings.SuccessURL)
                           .Replace("[title]", "Email Confirmation")
                           .Replace("[header]", "Email Confirmation")
                           .Replace("[year]", DateTime.Today.Year.ToString());

        EmailRequest email = new(to, "Email Confirmation", mailText);
        await _iNotificationService.SendEmail(email, cancellationToken);
    }
    private async Task SendConfirmationEmail(string to, Guid id, CancellationToken? cancellationToken)
    {
        string? filePath = Path.Combine(_env.WebRootPath, "Templates", "EmailConfirmation.html");
        StreamReader? str = new(filePath);
        string? mailText = await str.ReadToEndAsync();
        str.Close();

        mailText = mailText.Replace("[url]", _emailSettings.EmailConfirmationURL + id.ToString())
                           .Replace("[year]", DateTime.Today.Year.ToString());

        EmailRequest email = new(to, "Complete Registration", mailText);
        await _iNotificationService.SendEmail(email, cancellationToken);
    }
}