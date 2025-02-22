namespace LibrarySystem.Application.Interfaces.Services;
public interface IIdentityService
{
    Guid? GetCurrentLoggedInUser();
    string GetCurrentLoggedInEmail();
}
