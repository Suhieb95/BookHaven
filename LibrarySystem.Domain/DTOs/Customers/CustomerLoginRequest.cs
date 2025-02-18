using LibrarySystem.Domain.DTOs.BaseModels;
namespace LibrarySystem.Domain.DTOs.Customers;
public class CustomerLoginRequest(string emailAddress, string password) : LoginRequestBase(emailAddress, password);