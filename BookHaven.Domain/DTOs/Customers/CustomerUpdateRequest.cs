using BookHaven.Domain.DTOs.BaseModels;
using Microsoft.AspNetCore.Http;

namespace BookHaven.Domain.DTOs.Customers;
public class CustomerUpdateRequest : UpdateRequestBase
{
    public CustomerUpdateRequest() : base() { }
    public CustomerUpdateRequest(string emailAddress, string password, string userName, string? imageUrl = null, IFormFile? image = null)
         : base(emailAddress, password, userName, imageUrl, image)
    {
    }
}