using LibrarySystem.Application.Books;
using Microsoft.Extensions.DependencyInjection;
namespace LibrarySystem.Application.DependencyInjection.DependencyInjection;
internal static class ServiceDependencyInjection
{
    internal static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBookApplicationService, BookApplicationService>();
        return services;
    }
}
