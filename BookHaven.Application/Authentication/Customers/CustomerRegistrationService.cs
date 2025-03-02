using BookHaven.Application.Authentication.Common;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs.Auth;
using BookHaven.Domain.DTOs.Customers;
using BookHaven.Domain.Specification;
using BookHaven.Domain.Specification.Customers;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace BookHaven.Application.Authentication.Customers;
public class CustomerRegistrationService(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator, INotificationService notificationService
    , IOptions<EmailSettings> emailSettings, IWebHostEnvironment env, IPasswordHasher passwordHasher) : ICustomerRegistrationService
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly INotificationService _notificationService = notificationService;
    private readonly IWebHostEnvironment _env = env;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    public async Task<Result<bool>> Register(CustomerRegisterRequest request, CancellationToken? cancellationToken = null)
    {
        if (await IsEmailAddressInUse(request.EmailAddress, cancellationToken))
            return Result<bool>.Failure(new Error("Email Address exists, Please Login.", BadRequest, "Email Address Exists"));

        (bool flowControl, Result<bool>? result) = await CheckUserNameExistence.ValidateUserName<bool>(_unitOfWork, request.UserName, cancellationToken: cancellationToken);
        if (flowControl == false) return result!;

        HashPassword(request);
        Guid id = await _unitOfWork.Customers.Add(request, cancellationToken);
        await SendEmailConfirmationToken(request.EmailAddress, id, cancellationToken);

        return Result<bool>.Success(true);
    }
    public async Task<Result<bool>> ConfirmEmailAddress(Guid id, CancellationToken? cancellationToken = null)
    {
        Customer? currentUser = await GetUser(new GetCustomerById(id), cancellationToken);
        if (currentUser is null)
            return Result<bool>.Failure(new Error("User Doesn't Exists.", BadRequest, "Invalid User"));

        if (!currentUser.HasValidEmailConfirmationToken())
            return Result<bool>.Failure(new Error("Token Has Expired.", BadRequest, "Invalid Token"));

        await _unitOfWork.UserSecurity.UpdateEmailConfirmationToken(id, cancellationToken);
        await EmailHelpers.SendEmailConfirmedNotify(currentUser!.EmailAddress, _emailSettings.SuccessURL, _env, cancellationToken, _notificationService);
        return Result<bool>.Success(true);
    }
    private void HashPassword(CustomerRegisterRequest request)
       => request.SetPassword(_passwordHasher.Hash(request.Password));
    private async Task SendEmailConfirmationToken(string emailAddress, Guid result, CancellationToken? cancellationToken)
    {
        Customer? currentUser = await GetUser(new GetCustomerByEmailAddress(emailAddress), cancellationToken);
        if (currentUser is not null && currentUser.HasValidEmailConfirmationToken())
            return;

        EmailConfirmationResult emailConfirmation = _jwtTokenGenerator.GenerateEmailConfirmationToken(result);
        await _unitOfWork.UserSecurity.SaveEmailConfirmationToken(emailConfirmation, cancellationToken);
        await EmailHelpers.SendConfirmationEmail(currentUser!.EmailAddress, result, _emailSettings.EmailConfirmationURL, _env, cancellationToken, _notificationService);
    }
    private async Task<bool> IsEmailAddressInUse(string emailAddress, CancellationToken? cancellationToken)
    {
        Customer? currentUser = await GetUser(new GetCustomerByEmailAddress(emailAddress), cancellationToken);
        return currentUser is not null and { IsActive: true };
    }
    private async Task<T?> GetUser<T>(Specification<T> specification, CancellationToken? cancellationToken)
       => await _unitOfWork.Customers.GetBy(specification, cancellationToken);
}