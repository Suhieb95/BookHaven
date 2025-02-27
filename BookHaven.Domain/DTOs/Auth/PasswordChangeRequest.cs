namespace BookHaven.Domain.DTOs.Auth;

public class PasswordChangeRequest
{
    public required Guid Id { get; init; }
    public required string Password { get; set; } = default!;
    public void SetPassword(string hashedPassword) => Password = hashedPassword;
}
