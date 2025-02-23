using Microsoft.AspNetCore.Authorization;

namespace LibrarySystem.API.Utilities;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
internal sealed class HasPermissionAttribute : AuthorizeAttribute
{
    internal HasPermissionAttribute(Permission permission, EntityName entityName) :
     base(policy: $"{Convert.ToString(entityName)!}.{Convert.ToString(permission)!}")
    {
    }
}
