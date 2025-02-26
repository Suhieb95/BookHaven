using BookHaven.API.Middlewares;
using Scalar.AspNetCore;

namespace BookHaven.API;
internal static class WebApplicationExtentions
{
    internal static WebApplication AddAPIUI(this WebApplication application)
    {
        if (application.Environment.IsDevelopment())
        {
            application.MapScalarApiReference();
            application.MapOpenApi();
            application.UseDeveloperExceptionPage();
            application.UseSwagger();
            application.UseSwaggerUI();
        }
        else
        {
            application.UseMiddleware<ContentSecurityPolicyMiddleware>();
            application.UseExceptionHandler("/error");
        }

        return application;
    }
    internal static WebApplication BuildApplication(this WebApplication application, IConfiguration configuration)
    {
        var originCorsPolicy = configuration.GetSection(SpecifiedOriginCorsPolicy.SectionName).Get<SpecifiedOriginCorsPolicy>();

        if (originCorsPolicy is null)
            ArgumentNullException.ThrowIfNull(originCorsPolicy);

        bool isDevelopment = application.Environment.IsDevelopment();

        application.UseReferrerPolicy(opt => opt.NoReferrer());
        application.UseXContentTypeOptions();
        application.UseXXssProtection(opt => opt.EnabledWithBlockMode());
        application.UseXfo(opt => opt.Deny());
        application.UseCspReportOnly(opt => opt
                   .BlockAllMixedContent()
                   .FormActions(s => s.Self()
                   .CustomSources(isDevelopment ? originCorsPolicy!.LocalURL : originCorsPolicy!.ProductionURL))
                   .FrameAncestors(s => s.Self()));

        application.AddAPIUI();
        application.UseCors(isDevelopment ? originCorsPolicy!.LocalPolicyName : originCorsPolicy!.ProductionPolicyName); // CORS first
        application.MapHealthChecks("/healthz");
        application.UseRateLimiter();
        application.UseRequestTimeouts();
        application.UseHttpsRedirection();
        application.UseExceptionHandler("/error");

        application.UseAuthentication();
        application.UseAuthorization();

        application.MapControllers();
        application.MapStaticAssets();

        return application;
    }
}
