using FluentValidation;
using FluentValidation.AspNetCore;
using LibrarySystem.API;
using LibrarySystem.API.Common;
using LibrarySystem.API.Handlers;
using LibrarySystem.Application;
using LibrarySystem.Infrastructure.DependencyInjections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddInfrastructure(builder.Configuration, builder.Environment.IsDevelopment())
                .AddApplication(builder.Configuration);
                
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning(options =>
   {
       options.AssumeDefaultVersionWhenUnspecified = true;
       options.DefaultApiVersion = new ApiVersion(1, 0);
       options.ReportApiVersions = false;
       options.ApiVersionReader = new UrlSegmentApiVersionReader();
   });
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
builder.Services.AddHttpContextAccessor();
builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();
// builder.Services.AddControllers(options => options.Filters.Add<JwtValidationFilter>());
// builder.Services.AddScoped<LastLoginFilter>();
builder.Services.AddRouting(opt =>
{
    opt.LowercaseUrls = true; // lowercase routes 
    opt.LowercaseQueryStrings = true; // query string keys are automatically converted to lowercase
});
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthoriztionHandler>();

var app = builder.Build();
{
    app.BuildApplication(builder.Configuration).Run();
}
