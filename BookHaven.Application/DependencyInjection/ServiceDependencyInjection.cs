using BookHaven.Application.Authentication.Customers;
using BookHaven.Application.Authentication.Users;
using BookHaven.Application.Books;
using BookHaven.Application.Genres;
using Microsoft.Extensions.DependencyInjection;
namespace BookHaven.Application.DependencyInjection;
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
