namespace LibrarySystem.Infrastructure.Parameters.Customers;
internal class PasswordChangeParameters
{
    public required Guid Id { get; init; }
    public required string Password { get; init; }
}