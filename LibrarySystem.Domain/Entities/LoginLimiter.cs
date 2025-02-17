using LibrarySystem.Domain.BaseModels;

namespace InventoryManagement.Domain.Entities;

public class LoginLimiter : LimiterSettings
{
    public const string SectionName = "LoginLimiter";

}
