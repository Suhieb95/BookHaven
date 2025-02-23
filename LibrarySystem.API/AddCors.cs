namespace LibrarySystem.API;
internal static class AddCors
{
    internal static IServiceCollection AddCorsPolicy(this IServiceCollection services, ConfigurationManager configureManager)
    {
        ArgumentNullException.ThrowIfNull(services);

        var anyCors = new AnyCorsPolicy();
        configureManager.Bind(AnyCorsPolicy.SectionName, anyCors);
        services.AddSingleton(anyCors);

        var specifiedOrigin = new SpecifiedOriginCorsPolicy();
        configureManager.Bind(SpecifiedOriginCorsPolicy.SectionName, specifiedOrigin);
        services.AddSingleton(specifiedOrigin);

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
