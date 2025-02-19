using LibrarySystem.Domain.BaseModels.User;
namespace LibrarySystem.Domain.DTOs.Customers;
public class CustomerLoginRequest(string emailAddress, string password) : LoginRequestBase(emailAddress, password);