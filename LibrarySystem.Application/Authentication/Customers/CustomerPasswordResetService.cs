using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.DTOs.Auth;
using LibrarySystem.Domain.Specification;
using LibrarySystem.Domain.Specification.Customers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace LibrarySystem.Application.Authentication.Customers;
public class CustomerPasswordResetService(IUnitOfWork _unitOfWork, IOptions<EmailSettings> emailSettings, IPasswordHasher _passwordHasher, INotificationService _notificationService, IWebHostEnvironment _env, IJwtTokenGenerator _jwtTokenGenerator)
    : ICustomerPasswordResetService
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;
    public async Task<Result<bool>> ChangePassword(PasswordChangeRequest request, CancellationToken? cancellationToken)
    {
        Result<bool> result = await VerifyToken(request.UserId, cancellationToken);
        if (!result.IsSuccess)
            return result;

        request.SetPasswprd(_passwordHasher.Hash(request.Password));
        await _unitOfWork.Customers.UpdatePassowordResetToken(request, cancellationToken);
        Customer? customer = await GetCustomer(new GetCustomerById(request.UserId), cancellationToken);
        await NotifyCustomer(customer!.EmailAddress, cancellationToken);

        return Result<bool>.Success(true);
    }
    public async Task<Result<bool>> ResetPassword(string emailAddress, CancellationToken? cancellationToken)
    {
        Customer? customer = await GetCustomer(new GetCustomerByEmailAddress(emailAddress), cancellationToken);
        if (customer is null || customer is { IsActive: false })
            return Result<bool>.Failure(new("Customer Doesn't With this Exists.", BadRequest, "Invalid Customer"));

        ResetPasswordResult resetPassword = _jwtTokenGenerator.GeneratePasswordResetToken(emailAddress);
        await _unitOfWork.Customers.SavePassowordResetToken(resetPassword, cancellationToken);
        await SendResetLink(customer.Id, customer.EmailAddress, cancellationToken);

        return Result<bool>.Success(true);
    }
    public async Task<Result<bool>> VerifyToken(Guid id, CancellationToken? cancellationToken = null)
    {
        Customer? customer = await GetCustomer(new GetCustomerById(id), cancellationToken);
        if (customer is null || customer is { IsActive: false })
            return Result<bool>.Failure(new("Customer With this Email Address Doesn't Exists.", BadRequest, "Invalid Customer"));

        if (!customer.HasValidRestPasswordToken())
            return Result<bool>.Failure(new("Token Has Expired, Please request password change again.", BadRequest, "Invalid Token"));

        return Result<bool>.Success(true);
    }
    private async Task NotifyCustomer(string to, CancellationToken? cancellationToken)
    {
        string templatePath = Path.Combine(_env.WebRootPath, "Templates", "SuccessTemplate.html");
        StreamReader str = new(templatePath);
        string content = await str.ReadToEndAsync();
        str.Close();

        content = content.Replace("[msg]", $"Your Password Has been Successfully Changed.")
                                  .Replace("[url]", _emailSettings.SuccessURL)
                                  .Replace("[title]", "Password Changed")
                                  .Replace("[header]", "Password Changed")
                                  .Replace("[year]", DateTime.Today.Year.ToString());

        EmailRequest email = new(to, "Password Changed", content);
        await _notificationService.SendEmail(email, cancellationToken);
    }
    private async Task<Customer?> GetCustomer(Specification specification, CancellationToken? cancellationToken)
        => (await _unitOfWork.Customers.GetAll(specification, cancellationToken)).FirstOrDefault();
    private async Task SendResetLink(Guid id, string to, CancellationToken? cancellationToken)
    {
        string templatePath = Path.Combine(_env.WebRootPath, "Templates", "PasswordTemplate.html");
        StreamReader str = new(templatePath);
        string content = await str.ReadToEndAsync(cancellationToken: cancellationToken ?? CancellationToken.None);
        str.Close();

        content = content.Replace("[url]", _emailSettings.ResetPasswordURL + id.ToString())
                         .Replace("[year]", DateTime.Now.Year.ToString());

        EmailRequest email = new(to, "Password Reset", content);
        await _notificationService.SendEmail(email, cancellationToken);
    }
}