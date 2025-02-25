using BookHaven.Domain.BaseModels;
namespace BookHaven.Domain.Entities;
public class JwtSettings : JwtSettingsBase
{
    public const string SectionName = "JwtSettings";
    public int ExpiryMinutes { get; init; }
    public static string GetIssuedAt()
        => new DateTimeOffset(IssuedAt).ToUnixTimeSeconds().ToString();
}
