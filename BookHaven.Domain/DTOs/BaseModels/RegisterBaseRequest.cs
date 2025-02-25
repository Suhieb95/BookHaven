namespace BookHaven.Domain.DTOs.BaseModels;
public abstract class RegisterBaseRequest
{
    public RegisterBaseRequest(string emailAddress, string userName)
    {
        EmailAddress = NormalizeEmail(emailAddress);
        UserName = userName.Trim();
    }
    public string EmailAddress { get; }
    public string UserName { get; }
    private static string NormalizeEmail(string emailAddress) => emailAddress.Trim().ToLower();
}