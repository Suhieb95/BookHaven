using BookHaven.API;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices(builder.Configuration, builder.Environment.EnvironmentName, builder.Environment.IsDevelopment());
builder.Services.AddOpenApi(opt =>
{
    opt.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

var app = builder.Build();
app.BuildApplication(builder.Configuration).Run();