using LibrarySystem.Application.Interfaces;
using LibrarySystem.Domain.DTOs.Auth;
using LibrarySystem.Domain.DTOs.Customers;
using LibrarySystem.Domain.DTOs.Users;
using LibrarySystem.Infrastructure.Parameters.Person;

namespace LibrarySystem.Infrastructure.Mappings.Person;
internal static class Mappings
{
    internal static UpdatePersonParameters ToParameter(this CustomerUpdateRequest request)
        => new()
        {
            EmailAddress = request.EmailAddress,
            Password = request.Password,
            UserName = request.UserName,
            ImageUrl = request.ImageUrl
        };
    internal static CreatePersonParameters ToParameter(this CustomerRegisterRequest request, IDateTimeProvider dateTimeProvider)
      => new()
      {
          Id = Guid.CreateVersion7(dateTimeProvider.UtcNow),
          UserName = request.UserName,
          EmailAddress = request.EmailAddress,
          Password = request.Password,
          IsActive = false,
          IsVerified = false
      };
    internal static UpdatePersonParameters ToParameter(this InternalUserUpdateRequest request)
   => new()
   {
       EmailAddress = request.EmailAddress,
       Password = request.Password,
       UserName = request.UserName,
       ImageUrl = request.ImageUrl
   };
    internal static CreateInternalUserParameters ToParameter(this InternalUserRegisterRequest request, IDateTimeProvider dateTimeProvider)
      => new()
      {
          Id = Guid.CreateVersion7(dateTimeProvider.UtcNow),
          UserName = request.UserName,
          EmailAddress = request.EmailAddress,
          IsActive = false,
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
         ResetPasswordToken = passwordResult.PasswordResetToken,
         ResetPasswordTokenExpiry = passwordResult.PasswordResetTokenExpiry
     };
    internal static PasswordChangeParameters ToParameter(this PasswordChangeRequest passwordChangeRequest)
      => new()
      {
          Id = passwordChangeRequest.UserId,
          Password = passwordChangeRequest.Password
      };
}