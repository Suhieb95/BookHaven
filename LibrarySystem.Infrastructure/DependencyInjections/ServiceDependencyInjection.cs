using InventoryManagement.Infrastructure.Services.EmailService;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Infrastructure.Services;
using LibrarySystem.Infrastructure.Services.BookService;
using LibrarySystem.Infrastructure.Services.CustomerService;
using Microsoft.Extensions.DependencyInjection;
namespace LibrarySystem.Infrastructure.DependencyInjections;
internal static class ServiceDependencyInjection
{
    internal static IServiceCollection AddServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IFileService, FileService>();
        return services;
    }
}
