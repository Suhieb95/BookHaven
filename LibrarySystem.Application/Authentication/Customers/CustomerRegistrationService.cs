using LibrarySystem.Application.Authentication.Common;
using LibrarySystem.Application.Interfaces.Services;
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
        await EmailHelpers.SendEmailConfirmedNotify(currentUser!.EmailAddress, _emailSettings.SuccessURL, _env, cancellationToken, _iNotificationService);
        return Result<bool>.Success(true);
    }
    private void HashPassword(CustomerRegisterRequest request)
       => request.SetPassword(_passwordHasher.Hash(request.Password));
    private async Task SendEmailConfirmationToken(string emailAddress, Guid result, CancellationToken? cancellationToken)
    {
        List<Customer>? res = await _iUnitOfWork.Customers.GetAll(new GetCustomerByEmailAddress(emailAddress), cancellationToken);
        Customer? currentUser = res.FirstOrDefault();
        if (currentUser is not null && currentUser.HasValidEmailConfirmationToken())
            return;

        EmailConfirmationResult emailConfirmation = _jwtTokenGenerator.GenerateEmailConfirmationToken(result);
        await _iUnitOfWork.Customers.SaveEmailConfirmationToken(emailConfirmation, cancellationToken);
        await EmailHelpers.SendConfirmationEmail(currentUser!.EmailAddress, result, _emailSettings.EmailConfirmationURL, _env, cancellationToken, _iNotificationService);
    }
    private async Task<bool> IsEmailAddressInUse(string emailAddress, CancellationToken? cancellationToken)
    {
        List<Customer> res = await _iUnitOfWork.Customers.GetAll(new GetCustomerByEmailAddress(emailAddress), cancellationToken);
        Customer? currentUser = res.FirstOrDefault();
        if (currentUser is not null and { IsActive: true })
            return true;

        return false;
    }
}