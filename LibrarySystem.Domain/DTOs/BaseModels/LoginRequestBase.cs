namespace LibrarySystem.Domain.DTOs.BaseModels;
public abstract class LoginRequestBase(string emailAddress, string password)
{
    public string EmailAddress { get; init; } = emailAddress;
    public string Password { get; init; } = password;
}