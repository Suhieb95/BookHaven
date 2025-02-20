using LibrarySystem.Domain.BaseModels.User;
namespace LibrarySystem.Domain.Entities;
public class Customer : PersonBase, IPersonEntity
{
    public bool IsVerified { get; set; }
    public string? ResetPasswordToken { get; set; }
    public DateTime ResetPasswordTokenExpiry { get; set; }
    public string? EmailAddressConfirmationToken { get; set; }
    public DateTime EmailAddressConfirmationTokenExpiry { get; set; }
    public string Password { get; set; } = default!;
    public bool IsActive { get; set; }
    public DateTime? LastLogin { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool HasValidRestPasswordToken() => ResetPasswordTokenExpiry > DateTime.Now && ResetPasswordToken != null;
    public bool HasValidEmailConfirmationToken() => EmailAddressConfirmationTokenExpiry > DateTime.Now && EmailAddressConfirmationToken != null;
};
