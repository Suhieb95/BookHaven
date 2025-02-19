using LibrarySystem.Application.Interfaces;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Infrastructure.Authentication;
using LibrarySystem.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using static LibrarySystem.Application.Helpers.Extensions;

namespace LibrarySystem.Infrastructure.DependencyInjections;
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
