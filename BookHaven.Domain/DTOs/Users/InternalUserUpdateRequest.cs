using BookHaven.Domain.DTOs.BaseModels;
using Microsoft.AspNetCore.Http;

namespace BookHaven.Domain.DTOs.Users;
public class InternalUserUpdateRequest : LoginRequestBase
{
    public InternalUserUpdateRequest() : base() { } // Parameterless constructor for model binding

    public InternalUserUpdateRequest(string emailAddress, string password, string userName, string? imageUrl = null, IFormFile? image = null)
        : base(emailAddress, password)
    {
        UserName = userName;
        ImageUrl = imageUrl;
        Image = image;
    }
    public required string UserName { get; init; }
    public string? ImageUrl { get; set; }
    public IFormFile? Image { get; init; }
}