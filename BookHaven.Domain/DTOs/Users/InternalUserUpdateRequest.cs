using BookHaven.Domain.DTOs.BaseModels;
using Microsoft.AspNetCore.Http;

namespace BookHaven.Domain.DTOs.Users;
public class InternalUserUpdateRequest : UpdateRequestBase
{
    public InternalUserUpdateRequest() : base() { } // Parameterless constructor for model binding

    public InternalUserUpdateRequest(string emailAddress, string password, string userName, string? imageUrl = null, IFormFile? image = null)
        : base(emailAddress, password, userName, imageUrl, image)
    {
    }
}