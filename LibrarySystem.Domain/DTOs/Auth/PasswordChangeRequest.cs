namespace LibrarySystem.Domain.DTOs.Auth;

public class PasswordChangeRequest
{
    public required Guid UserId { get; init; }
    public string Password { get; private set; } = default!;
    public void SetPasswprd(string hashedPassword) => Password = hashedPassword;
}
