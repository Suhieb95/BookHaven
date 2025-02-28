using Microsoft.Extensions.Options;

namespace BookHaven.API;
internal static class AddCors
{
    internal static IServiceCollection AddCorsPolicy(this IServiceCollection services, ConfigurationManager configureManager)
    {
        ArgumentNullException.ThrowIfNull(services);

        AnyCorsPolicy anyCors = configureManager.GetSection(AnyCorsPolicy.SectionName).Get<AnyCorsPolicy>()!;

        SpecifiedOriginCorsPolicy? specifiedOrigin = new();
        configureManager.Bind(SpecifiedOriginCorsPolicy.SectionName, specifiedOrigin);
        services.AddSingleton(Options.Create(specifiedOrigin));

        services.AddCors(opt =>
        {
            opt.AddPolicy(anyCors.PolicyName!, builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            opt.AddPolicy(specifiedOrigin.ProductionPolicyName!, builder => builder.WithOrigins(specifiedOrigin.ProductionURL!)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            opt.AddPolicy(specifiedOrigin.LocalPolicyName!, builder => builder.WithOrigins(specifiedOrigin.LocalURL!)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

        });

        return services;
    }
}
