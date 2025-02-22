namespace LibrarySystem.Infrastructure.Parameters.Customers;
internal class PasswordResetTokenParameters
{
    public required string EmailAddress { get; init; }
    public required string PasswordResetToken { get; init; }
    public required DateTime PasswordResetTokenExpiry { get; init; }
}
