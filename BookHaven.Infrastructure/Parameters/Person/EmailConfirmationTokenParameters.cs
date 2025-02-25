namespace BookHaven.Infrastructure.Parameters.Person;
internal class EmailConfirmationTokenParameters
{
    public required Guid Id { get; init; }
    public required string VerifyEmailToken { get; init; } = default!;
    public required DateTime VerifyEmailTokenExpiry { get; init; }
}
