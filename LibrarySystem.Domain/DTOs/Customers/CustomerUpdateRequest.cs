using LibrarySystem.Domain.DTOs.BaseModels;
using Microsoft.AspNetCore.Http;

namespace LibrarySystem.Domain.DTOs.Customers;
public class CustomerUpdateRequest : LoginRequestBase
{
    public CustomerUpdateRequest() : base() { }
    public CustomerUpdateRequest(string emailAddress, string password, string userName, string? imageUrl = null, IFormFile? image = null) : base(emailAddress, password)
    {
        UserName = userName;
        ImageUrl = imageUrl;
        Image = image;
    }
    public required string UserName { get; init; }
    public string? ImageUrl { get; set; }
    public IFormFile? Image { get; init; }
    public bool IsValidToDeleteImage() => Image is null && ImageUrl is null;
}

