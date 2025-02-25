using BookHaven.Domain.DTOs.BaseModels;
namespace BookHaven.Domain.DTOs.Customers;
public class CustomerLoginRequest(string emailAddress, string password) : LoginRequestBase(emailAddress, password);