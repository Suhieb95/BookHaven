namespace LibrarySystem.Domain.DTOs.Users;
public class ResponseBase
{
    public string EmailAddress { get; set; } = default!;
    public string UserName { get; set; } = default!;
};