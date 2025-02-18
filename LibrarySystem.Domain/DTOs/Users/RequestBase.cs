namespace LibrarySystem.Domain.DTOs.Users;
public class RequestBase
{
    public string EmailAddress { get; set; } = default!;
    public string Password { get; set; } = default!;
}
