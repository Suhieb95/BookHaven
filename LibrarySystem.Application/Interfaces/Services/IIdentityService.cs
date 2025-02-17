namespace LibrarySystem.Application.Interfaces;
public interface IIdentityService
{
    Guid? GetCurrentLoggedInUser();
    string GetCurrentLoggedInEmail();
}
