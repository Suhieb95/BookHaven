namespace LibrarySystem.Domain.DTOs.Users;
public class ResponseBase
{
    public string EmailAddress { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Token { get; set; } = default!;
    public string string ImagePath { get; set; } = default!;
    public List<string> Permissions { get; set; } = default!;
};