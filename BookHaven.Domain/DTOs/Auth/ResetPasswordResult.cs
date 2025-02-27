namespace BookHaven.Domain.DTOs.Auth;
public record class ResetPasswordResult
{
    public ResetPasswordResult(string emailAddress, string resetPasswordToken, DateTime resetPasswordTokenExpiry)
    {
        EmailAddress = emailAddress;
        ResetPasswordToken = resetPasswordToken;
        ResetPasswordTokenExpiry = resetPasswordTokenExpiry;
    }
    public ResetPasswordResult() { }
    public string EmailAddress { get; init; } = default!;
    public string ResetPasswordToken { get; init; } = default!;
    public DateTime ResetPasswordTokenExpiry { get; init; }
}