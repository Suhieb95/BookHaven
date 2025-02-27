using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.Entities;
using BookHaven.Infrastructure.Authentication;
using BookHaven.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using static BookHaven.Application.Helpers.Extensions;

namespace BookHaven.Infrastructure.DependencyInjections;
internal static class AuthDependencyInjection
{
    internal static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton<IRefreshTokenCookieSetter, RefreshTokenCookieSetter>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IRefreshTokenValidator, RefreshTokenValidator>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddJwtTokenAuthentication(configuration);

        return services;
    }
    private static IServiceCollection AddJwtTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);
        services.AddSingleton(Options.Create(jwtSettings));

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = ValidateJwtToken(jwtSettings);
        });

        return services;
    }
}
