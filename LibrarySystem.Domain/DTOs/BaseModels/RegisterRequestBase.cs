namespace LibrarySystem.Domain.DTOs.BaseModels;
public abstract class RegisterRequestBase(string emailAddress, string password, string userName)
{
    public string EmailAddress { get; protected init; } = emailAddress;
    public string Password { get; protected set; } = password;
    public string UserName { get; protected init; } = userName;
}