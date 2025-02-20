using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.DTOs.Auth;
using LibrarySystem.Domain.DTOs.Customers;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Specification.Customers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace LibrarySystem.Application.Authentication.Customers;
public class CustomerRegisterationService(ICustomerService _customerService, IJwtTokenGenerator _jwtTokenGenerator, IEmailService _emailSender, IOptions<EmailSettings> _emailSettings, IWebHostEnvironment _env) : ICustomerRegisterationService
{
    private readonly EmailSettings _emailSettings = _emailSettings.Value;
    public async Task<Result<bool>> Register(CustomerRegisterRequest request, CancellationToken? cancellationToken = null)
    {
        if (await IsEmailAddressInUse(request.EmailAddress, cancellationToken))
            return Result<bool>.Failure(new Error("Email Address exists, Please Login.", BadRequest, "Email Address Exists"));

        Guid result = await _customerService.Add(request);
        EmailConfirmationResult emailConfirmation = _jwtTokenGenerator.GenerateEmailConfirmationToken(result);
        await _customerService.SaveConfirmationToken(emailConfirmation);
        await SendConfirmationEmail(request.EmailAddress, result);

        return Result<bool>.Success(true);
    }
    private async Task<bool> IsEmailAddressInUse(string emailAddress, CancellationToken? cancellationToken)
    {
        var res = await _customerService.GetAll(new GetCustomerByEmailAddress(emailAddress), cancellationToken);
        if (res.FirstOrDefault() != null)
            return true;

        return false;
    }
    private async Task SendConfirmationEmail(string to, Guid id)
    {
        string? filePath = Path.Combine(_env.WebRootPath, "Templates", "EmailConfirmation.html");
        var str = new StreamReader(filePath);
        string? mailText = str.ReadToEnd();
        str.Close();

        mailText = mailText.Replace("[url]", _emailSettings.EmailConfirmationURL + id.ToString())
                           .Replace("[year]", DateTime.Today.Year.ToString());

        EmailRequest email = new(
           to,
           "Complete Registeration",    
            mailText
        );
        await _emailSender.SendEmail(email);
    }
}
