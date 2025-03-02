namespace BookHaven.Domain.DTOs.BaseModels;
public abstract class LoginBaseRequest
{
    public LoginBaseRequest() { }
    public LoginBaseRequest(string emailAddress, string password)
    {
        EmailAddress = emailAddress;
        Password = password;
    }
    public required string EmailAddress { get; init; }
    public required string Password { get; set; }
    public void SetPassword(string password) => Password = password;
}