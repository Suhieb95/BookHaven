using BookHaven.Application.Interfaces.Repositories;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Infrastructure.Repositories;
using BookHaven.Infrastructure.Services;
using BookHaven.Infrastructure.Services.EmailService;
using BookHaven.Infrastructure.Services.Genres;

using Microsoft.Extensions.DependencyInjection;

namespace BookHaven.Infrastructure.DependencyInjections;
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
        services.AddScoped<IGenericSpecificationReadRepository, GenericSpecificationReadRepository>();
        return services;
    }
}
