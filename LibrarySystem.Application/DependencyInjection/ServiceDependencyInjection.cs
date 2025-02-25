using LibrarySystem.Application.Authentication.Customers;
using LibrarySystem.Application.Authentication.Users;
using LibrarySystem.Application.Books;
using LibrarySystem.Application.Genres;
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
        services.AddScoped<IGenreApplicationService, GenreApplicationService>();
        services.AddScoped<IUserRegistrationService, UserRegistrationService>();
        services.AddScoped<IUserLoginService, UserLoginService>();
        services.AddScoped<IUserResetPassword, UserResetPassword>();
        services.AddScoped<IUserUpdateService, UserUpdateService>();
        services.AddScoped<ICustomerUpdateService, CustomerUpdateService>();
        return services;
    }
}
