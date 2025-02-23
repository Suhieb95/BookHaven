namespace LibrarySystem.Domain.DTOs.BaseModels;
public abstract class RegisterRequestBase(string emailAddress, string password, string userName)
{
    public string EmailAddress { get; init; } = emailAddress;
    public string Password { get; set; } = password;
    public string UserName { get; init; } = userName;
    public void SetPassword(string password) => Password = password;
}