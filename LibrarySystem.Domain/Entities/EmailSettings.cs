namespace LibrarySystem.Domain.Entities;

public class EmailSettings
{
    public const string SectionName = "EmailSenderSettings";
    public required string SmtpHost { get; init; }
    public required int SmtpPort { get; init; }
    public required string EmailAddress { get; init; }
    public required string UserName { get; init; }
    public required string Password { get; init; }
    public string ResetPasswordURL { get; init; } = null!;
    public string EmailConfirmationURL { get; init; } = null!;
    public string SuccessURL { get; init; } = null!;
}
