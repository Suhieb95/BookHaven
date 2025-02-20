namespace LibrarySystem.Domain.DTOs.Auth;

public class PasswordChangeRequest
{
    public required Guid UserId { get; init; }
    public required string Password { get; set; }
}
