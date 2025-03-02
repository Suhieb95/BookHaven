using Microsoft.AspNetCore.Http;
namespace BookHaven.Domain.DTOs.BaseModels;
public abstract class UpdateRequestBase
{
    public UpdateRequestBase() { }
    public UpdateRequestBase(string emailAddress, string password, string userName, string? imageUrl = null, IFormFile? image = null)
    {
        EmailAddress = emailAddress;
        Password = password;
        ImageUrl = imageUrl;
        Image = image;
        UserName = userName;
    }
    public required string EmailAddress { get; init; }
    public required string Password { get; set; }
    public required string UserName { get; init; }
    public string? ImageUrl { get; set; }
    public IFormFile? Image { get; init; }
    public void SetPassword(string password) => Password = password;
}