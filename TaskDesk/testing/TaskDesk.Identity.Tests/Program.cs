using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TaskDesk.Domain;
using TaskDesk.Identity;
using TaskDesk.Migrations;
using TaskDesk.Shared;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ContentRootPath = AppContext.BaseDirectory
});

RegisterServices(builder);

var app = builder.Build();

ConfigureApplication(app);

static void RegisterServices(WebApplicationBuilder builder)
{
    builder.Services.AddDomainDependencies(builder.Configuration);
    builder.Services.AddMigrationsDependencies(builder.Configuration);
    builder.Services.AddSharedDependencies();
    builder.Services.AddSharedSerializer();
    builder.Services.AddSharedCors();
    builder.Services.AddIdentityDependencies(builder.Configuration);

    builder.Services.AddEndpointsApiExplorer();
}

static void ConfigureApplication(WebApplication app)
{
    app.UseSharedSwagger();
    app.UseSharedCors();
    app.UseHttpsRedirection();
    app.AddIdentityDependencies();
    app.MapControllers();

    app.UseCookiePolicy();

    app.Run();
}