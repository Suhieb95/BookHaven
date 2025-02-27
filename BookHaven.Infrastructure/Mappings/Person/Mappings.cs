using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs.Auth;
using BookHaven.Domain.DTOs.Customers;
using BookHaven.Domain.DTOs.Users;
using BookHaven.Infrastructure.Parameters.Person;

namespace BookHaven.Infrastructure.Mappings.Person;
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
}
