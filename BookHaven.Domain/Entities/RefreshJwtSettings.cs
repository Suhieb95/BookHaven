using BookHaven.Domain.BaseModels;

namespace BookHaven.Domain.Entities;

public sealed class RefreshJwtSettings : JwtSettingsBase
{
    public const string SectionName = "RefreshJwtSettings";
    public int ExpiryDays { get; init; }
}
