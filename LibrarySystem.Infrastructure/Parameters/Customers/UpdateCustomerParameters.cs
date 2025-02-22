namespace LibrarySystem.Infrastructure.Parameters.Customers;
internal class UpdateCustomerParameters
{
    public required string EmailAddress { get; init; }
    public required string Password { get; init; }
    public required string UserName { get; init; }
    public string? ImageUrl { get; init; }
}
