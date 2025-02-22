namespace LibrarySystem.Infrastructure.Parameters.Customers;
internal class EmailConfirmationTokenParameters
{
    public required Guid Id { get; init; }
    public required string VerifyEmailToken { get; init; } = default!;
    public required DateTime VerifyEmailTokenExpiry { get; init; }
}
