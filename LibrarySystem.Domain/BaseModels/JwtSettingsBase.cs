namespace LibrarySystem.Domain.BaseModels;
public abstract class JwtSettingsBase
{
    public string Secret { get; init; } = null!;
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public static DateTime IssuedAt => DateTime.Now;
}
