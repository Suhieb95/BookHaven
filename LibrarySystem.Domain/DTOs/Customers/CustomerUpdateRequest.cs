using Microsoft.AspNetCore.Http;

namespace LibrarySystem.Domain.DTOs.Customers;
public record class CustomerUpdateRequest(string EmailAddress, string Password, string UserName, string? ImageUrl = null, IFormFile? Image = null);
