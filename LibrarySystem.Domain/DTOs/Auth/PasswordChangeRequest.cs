namespace LibrarySystem.Domain.DTOs.Auth;

public class PasswordChangeRequest
{
    public required Guid UserId { get; init; }
    public string Password { get; set; } = default!;
    public void SetPasswprd(string hashedPassword) => Password = hashedPassword;
}
