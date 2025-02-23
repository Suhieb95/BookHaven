using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Infrastructure.Services;
using LibrarySystem.Infrastructure.Services.EmailService;
using LibrarySystem.Infrastructure.Services.Genres;
using Microsoft.Extensions.DependencyInjection;
namespace LibrarySystem.Infrastructure.DependencyInjections;
internal static class ServiceDependencyInjection
{
    internal static IServiceCollection AddServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IGenreService, GenreService>();

        return services;
    }
}   
