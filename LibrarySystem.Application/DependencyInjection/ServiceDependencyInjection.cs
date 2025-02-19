using LibrarySystem.Application.Authentication.Customers;
using LibrarySystem.Application.Books;
using Microsoft.Extensions.DependencyInjection;
namespace LibrarySystem.Application.DependencyInjection;
internal static class ServiceDependencyInjection
{
    internal static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBookApplicationService, BookApplicationService>();
        services.AddScoped<ICustomerRegisterationService, CustomerRegisterationService>();
        return services;
    }
}
