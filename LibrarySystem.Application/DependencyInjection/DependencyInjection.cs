using LibrarySystem.Application.Authentication;
using LibrarySystem.Application.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace LibrarySystem.Application.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
        services.AddScoped<IBookApplicationService, BookApplicationService>();
        services.AddServices();
        return services;
    }
}
