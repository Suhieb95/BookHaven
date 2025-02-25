using LibrarySystem.Domain.BaseModels.User;
using LibrarySystem.Domain.Enums;

namespace LibrarySystem.Domain.DTOs.Customers;

public class CustomerLoginResponse(string emailAddress, string userName, string? imageurl, string token, Guid id, string refreshToken)
 : AuthenticatedUserBase(emailAddress, userName, token, id, UserType.Customer, imageurl, refreshToken)
{
};