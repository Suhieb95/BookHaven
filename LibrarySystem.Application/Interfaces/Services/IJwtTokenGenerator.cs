using LibrarySystem.Domain.BaseModels.User;
using LibrarySystem.Domain.DTOs.Auth;
using LibrarySystem.Domain.Enums;
namespace LibrarySystem.Application.Interfaces.Services;
public interface IJwtTokenGenerator
{
    Task<string> GenerateAccessToken(PersonBase person, PersonType personType = PersonType.Customer);
    string GenerateRefreshToken(PersonBase person);
    ResetPasswordResult GeneratePasswordResetToken(string emailAddress);
    EmailConfirmationResult GenerateEmailConfirmationToken(Guid userId);
}

