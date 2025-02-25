using BookHaven.API;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices(builder.Configuration, builder.Environment.EnvironmentName, builder.Environment.IsDevelopment());

var app = builder.Build();
app.BuildApplication(builder.Configuration).Run();