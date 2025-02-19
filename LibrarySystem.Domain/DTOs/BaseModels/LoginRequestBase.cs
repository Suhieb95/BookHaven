namespace LibrarySystem.Domain.BaseModels.User;
public abstract class LoginRequestBase(string emailAddress, string password)
{
    public string EmailAddress { get; } = emailAddress;
    public string Password { get; } = password;
}