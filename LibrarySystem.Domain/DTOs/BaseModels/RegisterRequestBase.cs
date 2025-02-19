namespace LibrarySystem.Domain.DTOs.BaseModels;
public abstract class RegisterRequestBase(string emailAddress, string password, string userName)
{
    public string EmailAddress { get; } = emailAddress;
    public string Password { get; } = password;
    public string UserName { get; } = userName;
}