using LibrarySystem.Application.Authentication.Customers;
using LibrarySystem.Application.Books;
using LibrarySystem.Application.Genres;
using LibrarySystem.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
namespace LibrarySystem.Application.DependencyInjection;
internal static class ServiceDependencyInjection
{
    internal static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBookApplicationService, BookApplicationService>();
        services.AddScoped<ICustomerRegistrationService, CustomerRegistrationService>();
        services.AddScoped<ICustomerLoginService, CustomerLoginService>();
        services.AddScoped<ICustomerPasswordResetService, CustomerPasswordResetService>();
        return services;
    }
}
