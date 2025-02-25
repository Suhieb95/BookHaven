using BookHaven.Domain.BaseModels;

namespace BookHaven.Domain.Entities;

public class LoginLimiter : LimiterSettings
{
    public const string SectionName = "LoginLimiter";

}