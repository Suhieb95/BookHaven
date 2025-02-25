using LibrarySystem.Domain.BaseModels.User;
using LibrarySystem.Domain.DTOs.Auth;
using LibrarySystem.Domain.Enums;
namespace LibrarySystem.Application.Interfaces.Services;
public interface IJwtTokenGenerator
{
    string GenerateAccessToken(PersonBase person, UserType personType = UserType.Customer, string[]? roles = null, string[]? permissions = null);
    string GenerateRefreshToken(PersonBase person, UserType personType = UserType.Customer);
    ResetPasswordResult GeneratePasswordResetToken(string emailAddress);
    EmailConfirmationResult GenerateEmailConfirmationToken(Guid userId);
}

