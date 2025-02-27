namespace BookHaven.Domain.Entities;

public interface IPersonEntity
{
    string Password { get; }
    bool IsVerified { get; }
    bool IsActive { get; }
    DateTime? LastLogin { get; }
    DateTime CreatedAt { get; }
    string? ImageUrl { get; }
    string? ResetPasswordToken { get; }
    DateTime ResetPasswordTokenExpiry { get; }
    string? VerifyEmailToken { get; }
    DateTime VerifyEmailTokenExpiry { get; }
    bool HasValidRestPasswordToken();
    bool HasValidEmailConfirmationToken();
}