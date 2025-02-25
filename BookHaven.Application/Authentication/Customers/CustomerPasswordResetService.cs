using BookHaven.Application.Authentication.Common;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs.Auth;
using BookHaven.Domain.Specification;
using BookHaven.Domain.Specification.Customers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace BookHaven.Application.Authentication.Customers;
public class CustomerPasswordResetService(IUnitOfWork _unitOfWork, IOptions<EmailSettings> emailSettings, IPasswordHasher _passwordHasher, INotificationService _notificationService, IWebHostEnvironment _env, IJwtTokenGenerator _jwtTokenGenerator)
    : ICustomerPasswordResetService
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;
    public async Task<Result<bool>> ChangePassword(PasswordChangeRequest request, CancellationToken? cancellationToken)
    {
        Result<bool> result = await VerifyToken(request.UserId, cancellationToken);
        if (!result.IsSuccess)
            return result;

        request.SetPassword(_passwordHasher.Hash(request.Password));
        await _unitOfWork.Customers.UpdatePassowordResetToken(request, cancellationToken);
        Customer? customer = await GetCustomer(new GetCustomerById(request.UserId), cancellationToken);
        await EmailHelpers.SendPasswordChangedNotify(customer!.EmailAddress, _emailSettings.ResetPasswordURL, _env, cancellationToken, _notificationService);

        return Result<bool>.Success(true);
    }
    public async Task<Result<bool>> ResetPassword(string emailAddress, CancellationToken? cancellationToken)
    {
        Customer? customer = await GetCustomer(new GetCustomerByEmailAddress(emailAddress), cancellationToken);
        if (customer is null || customer is not null and { IsActive: false })
            return Result<bool>.Failure(new("Customer Doesn't With this Exists.", BadRequest, "Invalid Customer"));

        ResetPasswordResult resetPassword = _jwtTokenGenerator.GeneratePasswordResetToken(emailAddress);
        await _unitOfWork.Customers.SavePassowordResetToken(resetPassword, cancellationToken);
        await EmailHelpers.SendResetPasswordLink(customer!.EmailAddress, customer.Id, _emailSettings.ResetPasswordURL, _env, cancellationToken, _notificationService);
        return Result<bool>.Success(true);
    }
    public async Task<Result<bool>> VerifyToken(Guid id, CancellationToken? cancellationToken = null)
    {
        Customer? customer = await GetCustomer(new GetCustomerById(id), cancellationToken);
        if (customer is null || customer is not null and { IsActive: false })
            return Result<bool>.Failure(new("Customer With this Email Address Doesn't Exists.", BadRequest, "Invalid Customer"));

        if (!customer!.HasValidRestPasswordToken())
            return Result<bool>.Failure(new("Token Has Expired, Please request password change again.", BadRequest, "Invalid Token"));

        return Result<bool>.Success(true);
    }
    private async Task<Customer?> GetCustomer(Specification specification, CancellationToken? cancellationToken)
        => (await _unitOfWork.Customers.GetAll(specification, cancellationToken)).FirstOrDefault();
}