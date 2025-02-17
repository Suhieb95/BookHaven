using LibrarySystem.Domain.BaseModels;
namespace LibrarySystem.Domain.Entities;
public class JwtSettings : JwtSettingsBase
{
    public const string SectionName = "JwtSettings";
    public int ExpiryMinutes { get; init; }
    public static string GetIssuedAt()
        => new DateTimeOffset(IssuedAt).ToUnixTimeSeconds().ToString();
}
