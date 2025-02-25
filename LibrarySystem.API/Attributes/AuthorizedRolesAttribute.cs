namespace LibrarySystem.API.Attributes;
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class AuthorizedRolesAttribute : AuthorizeAttribute
{
    public AuthorizedRolesAttribute(params string[] roles)
        => Roles = string.Join(",", roles.Distinct());
}