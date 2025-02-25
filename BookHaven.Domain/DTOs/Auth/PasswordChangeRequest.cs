namespace BookHaven.Domain.DTOs.Auth;

public class PasswordChangeRequest
{
    public required Guid UserId { get; init; }
    public string Password { get; set; } = default!;
    public void SetPassword(string hashedPassword) => Password = hashedPassword;
}
