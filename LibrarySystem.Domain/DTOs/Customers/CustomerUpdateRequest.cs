using Microsoft.AspNetCore.Http;

namespace LibrarySystem.Domain.DTOs.Customers;
public record class CustomerUpdateRequest(string EmailAddress, string Password, string UserName, IFormFile? Image = null);
