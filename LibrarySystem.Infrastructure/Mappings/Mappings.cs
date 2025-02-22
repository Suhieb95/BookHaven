using LibrarySystem.Application.Interfaces;
using LibrarySystem.Domain.DTOs.Auth;
using LibrarySystem.Domain.DTOs.Customers;
using LibrarySystem.Infrastructure.Parameters.Customers;

namespace LibrarySystem.Infrastructure.Mappings;
internal static class Mappings
{
    internal static UpdateCustomerParameters ToParameter(this CustomerUpdateRequest request)
        => new()
        {
            EmailAddress = request.EmailAddress,
            Password = request.Password,
            UserName = request.UserName,
            ImageUrl = request.ImageUrl
        };
    internal static CreateCustomerParameters ToParameter(this CustomerRegisterRequest request, IDateTimeProvider dateTimeProvider)
      => new()
      {
          Id = Guid.CreateVersion7(dateTimeProvider.UtcNow),
          UserName = request.UserName,
          EmailAddress = request.EmailAddress,
          Password = request.Password,
          IsActive = true,
          IsVerified = false
      };
    internal static EmailConfirmationTokenParameters ToParameter(this EmailConfirmationResult result)
     => new()
     {
         Id = result.UserId,
         VerifyEmailToken = result.EmailAddressConfirmationToken,
         VerifyEmailTokenExpiry = result.EmailAddressConfirmationTokenExpiry
     };
    internal static PasswordResetTokenParameters ToParameter(this ResetPasswordResult passwordResult)
     => new()
     {
         EmailAddress = passwordResult.EmailAddress,
         PasswordResetToken = passwordResult.PasswordResetToken,
         PasswordResetTokenExpiry = passwordResult.PasswordResetTokenExpiry
     };
    internal static PasswordChangeParameters ToParameter(this PasswordChangeRequest passwordChangeRequest)
      => new()
      {
          UserId = passwordChangeRequest.UserId,
          Password = passwordChangeRequest.Password
      };
}