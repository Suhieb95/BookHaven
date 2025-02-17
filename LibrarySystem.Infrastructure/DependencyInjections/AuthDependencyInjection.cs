using System.Text;
using InventoryManagement.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LibrarySystem.Infrastructure.DependencyInjections;
internal static class AuthDependencyInjection
{
    internal static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        // services.AddSingleton<IRefreshTokenCookieSetter, RefreshTokenCookieSetter>();
        // services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        // services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        // services.AddScoped<IIdentityService, IdentityService>();
        // services.AddScoped<IRefreshTokenValidator, RefreshTokenValidator>();
        // services.AddScoped<IGoogleAuthenticationService, GoogleAuthenticationService>();

        services.AddJwtTokenAuthentication(configuration);
        return services;
    }
    private static IServiceCollection AddJwtTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        // var jwtSettings = new JwtSettings();
        // configuration.Bind(JwtSettings.SectionName, jwtSettings);
        // services.AddSingleton(Options.Create(jwtSettings));

        // services.AddAuthentication(opt =>
        // {
        //     opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //     opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        // })
        // .AddJwtBearer(opt =>
        // {
        //     opt.TokenValidationParameters = ValidateJwtToken(jwtSettings);
        // });

        return services;
    }
}
