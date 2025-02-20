using LibrarySystem.Domain.BaseModels.User;

namespace LibrarySystem.Domain.DTOs.Customers;

public class CustomerLoginResponse(string emailAddress, string userName, string imageurl, string token, Guid id)
 : AuthenticatedUserBase(emailAddress, userName, token, id)
{
    public string? ImageUrl { get; init; } = imageurl;
};