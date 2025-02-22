namespace LibrarySystem.Infrastructure.Parameters.Customers;
internal class PasswordChangeParameters
{
    public required Guid UserId { get; init; }
    public required string Password { get; init; }
}
