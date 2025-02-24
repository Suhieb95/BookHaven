using LibrarySystem.Domain.BaseModels.User;

namespace LibrarySystem.Domain.Entities;
public class User : PersonBase, IPersonEntity
{
    public string Password { get; set; } = default!;
    public bool IsActive { get; set; }
    public DateTime? LastLogin { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsVerified { get; set; }
    public string? ImageUrl { get; set; }
    public string? ResetPasswordToken { get; set; }
    public DateTime ResetPasswordTokenExpiry { get; set; }
    public string? VerifyEmailToken { get; set; }
    public DateTime VerifyEmailTokenExpiry { get; set; }
    public bool HasValidRestPasswordToken() => ResetPasswordTokenExpiry > DateTime.Now && ResetPasswordToken != null;
    public bool HasValidEmailConfirmationToken() => VerifyEmailTokenExpiry > DateTime.Now && VerifyEmailToken != null;
}
