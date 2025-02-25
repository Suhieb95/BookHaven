namespace BookHaven.Domain.DTOs.BaseModels;
public abstract class LoginRequestBase
{
    public LoginRequestBase() { }
    public LoginRequestBase(string emailAddress, string password)
    {
        EmailAddress = emailAddress;
        Password = password;
    }
    public required string EmailAddress { get; init; }
    public required string Password { get; set; }
    public void SetPassword(string password) => Password = password;
}