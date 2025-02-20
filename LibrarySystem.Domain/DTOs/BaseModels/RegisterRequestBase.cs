namespace LibrarySystem.Domain.DTOs.BaseModels;
public abstract class RegisterRequestBase(string emailAddress, string password, string userName)
{
    public string EmailAddress { get; init; } = emailAddress;
    public string Password { get; init; } = password;
    public string UserName { get; init; } = userName;
}