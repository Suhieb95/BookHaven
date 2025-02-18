using LibrarySystem.Domain.DTOs.BaseModels;

namespace LibrarySystem.Domain.DTOs.Customers;

public class CustomerLoginResponse(string emailAddress, string userName, string imageurl, string token)
 : AuthenticatedUserBase(emailAddress, userName, imageurl, token);