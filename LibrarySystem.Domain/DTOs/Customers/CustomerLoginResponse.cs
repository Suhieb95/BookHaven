using LibrarySystem.Domain.BaseModels.User;
using LibrarySystem.Domain.Enums;

namespace LibrarySystem.Domain.DTOs.Customers;

public class CustomerLoginResponse(string emailAddress, string userName, string imageurl, string token, Guid id)
 : AuthenticatedUserBase(emailAddress, userName, token, id, PersonType.Customer)
{
    public string? ImageUrl { get; init; } = imageurl;
};