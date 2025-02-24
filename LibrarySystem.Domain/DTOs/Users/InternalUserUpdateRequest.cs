using LibrarySystem.Domain.DTOs.BaseModels;
using Microsoft.AspNetCore.Http;

namespace LibrarySystem.Domain.DTOs.Users;
public class InternalUserUpdateRequest(string emailAddress, string password, string userName,
string? imageUrl = null, IFormFile? image = null) : LoginRequestBase(emailAddress, password)
{
    public string UserName { get; init; } = userName;
    public string? ImageUrl { get; init; } = imageUrl;
    public IFormFile? Image { get; init; } = image;
}