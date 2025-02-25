namespace BookHaven.Domain.DTOs.Auth;
public record ResetPasswordResult(
    string EmailAddress,
    string PasswordResetToken,
    DateTime PasswordResetTokenExpiry
);