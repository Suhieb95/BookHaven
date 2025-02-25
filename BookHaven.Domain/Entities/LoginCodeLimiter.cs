using BookHaven.Domain.BaseModels;

namespace BookHaven.Domain.Entities;

public class LoginCodeLimiter : LimiterSettings
{
    public const string SectionName = "LoginCodeLimiter";

}
