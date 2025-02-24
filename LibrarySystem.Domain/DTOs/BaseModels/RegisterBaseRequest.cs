namespace LibrarySystem.Domain.DTOs.BaseModels;
public abstract class RegisterBaseRequest(string emailAddress, string userName)
{
    public string EmailAddress { get; init; } = emailAddress;
    public string UserName { get; init; } = userName;
}