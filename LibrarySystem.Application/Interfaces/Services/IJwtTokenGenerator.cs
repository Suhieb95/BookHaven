using LibrarySystem.Domain.BaseModels.User;
using LibrarySystem.Domain.DTOs.Auth;
using LibrarySystem.Domain.Enums;
namespace LibrarySystem.Application.Interfaces.Services;
public interface IJwtTokenGenerator
{
    string GenerateAccessToken(PersonBase person, PersonType personType = PersonType.Customer, string[]? roles = null, string[]? permissions = null);
    string GenerateRefreshToken(PersonBase person);
    ResetPasswordResult GeneratePasswordResetToken(string emailAddress);
    EmailConfirmationResult GenerateEmailConfirmationToken(Guid userId);
}

