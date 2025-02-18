using LibrarySystem.Domain.BaseModels;

namespace LibrarySystem.Domain.Entities;

public class LoginLimiter : LimiterSettings
{
    public const string SectionName = "LoginLimiter";

}