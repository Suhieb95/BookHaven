namespace BookHaven.Domain.DTOs.Auth;
public record class EmailConfirmationResult
{
    public EmailConfirmationResult(Guid id, string verifyEmailToken, DateTime verifyEmailTokenExpiry)
    {
        Id = id;
        VerifyEmailToken = verifyEmailToken;
        VerifyEmailTokenExpiry = verifyEmailTokenExpiry;
    }
    public EmailConfirmationResult() { }
    public Guid Id { get; init; }
    public string VerifyEmailToken { get; init; } = default!;
    public DateTime VerifyEmailTokenExpiry { get; init; }
}