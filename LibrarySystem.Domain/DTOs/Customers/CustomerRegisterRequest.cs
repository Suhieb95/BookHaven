using LibrarySystem.Domain.DTOs.BaseModels;

namespace LibrarySystem.Domain.DTOs.Customers;
public class CustomerRegisterRequest(string emailAddress, string password, string userName) : RegisterRequestBase(emailAddress, password, userName);
