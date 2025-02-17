using LibrarySystem.Domain.BaseModels;

namespace LibrarySystem.Domain.Entities;

public sealed class RefreshJwtSettings : JwtSettingsBase
{
    public const string SectionName = "RefreshJwtSettings";
    public int ExpiryDays { get; init; }
}
