namespace LibrarySystem.API.Attributes;
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
internal sealed class AuthorizedAttribute : AuthorizeAttribute
{
    public AuthorizedAttribute() : base() { }
    public AuthorizedAttribute(params string[] roles)
        => Roles = string.Join(",", roles.Distinct());

    public AuthorizedAttribute(string[] roles, string policy)
    {
        if (roles.Length > 0)
            Roles = string.Join(",", roles.Distinct());

        Policy = policy;
    }
}