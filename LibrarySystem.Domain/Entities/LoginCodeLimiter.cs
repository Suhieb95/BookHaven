using LibrarySystem.Domain.BaseModels;

namespace LibrarySystem.Domain.Entities;

public class LoginCodeLimiter : LimiterSettings
{
    public const string SectionName = "LoginCodeLimiter";

}
