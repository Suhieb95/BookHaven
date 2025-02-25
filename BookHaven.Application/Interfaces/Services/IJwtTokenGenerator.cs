using BookHaven.Domain.BaseModels.User;
using BookHaven.Domain.DTOs.Auth;
using BookHaven.Domain.Enums;
namespace BookHaven.Application.Interfaces.Services;
public interface IJwtTokenGenerator
{
    string GenerateAccessToken(PersonBase person, UserType personType = UserType.Customer, string[]? roles = null, string[]? permissions = null);
    string GenerateRefreshToken(PersonBase person, UserType personType = UserType.Customer);
    ResetPasswordResult GeneratePasswordResetToken(string emailAddress);
    EmailConfirmationResult GenerateEmailConfirmationToken(Guid userId);
}

