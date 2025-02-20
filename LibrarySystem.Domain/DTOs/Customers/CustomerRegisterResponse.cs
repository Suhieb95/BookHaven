using LibrarySystem.Domain.BaseModels.User;

namespace LibrarySystem.Domain.DTOs.Customers;

public class CustomerRegisterResponse(string emailAddress, string userName, string token, Guid id)
 : AuthenticatedUserBase(emailAddress, userName, token, id);
