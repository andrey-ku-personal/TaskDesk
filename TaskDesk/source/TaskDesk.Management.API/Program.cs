using TaskDesk.Domain;
using TaskDesk.Migrations;
using TaskDesk.Shared;

var builder = WebApplication.CreateBuilder(args);

RegisterServices(builder);

var app = builder.Build();

ConfigureApplication(app);

app.Run();

static void RegisterServices(WebApplicationBuilder builder)
{
    builder.Services.AddDomainDependencies(builder.Configuration);
    builder.Services.AddMigrationsDependencies(builder.Configuration);
    builder.Services.AddManagementDependencies();
    builder.Services.AddSharedDependencies();
    builder.Services.AddSharedSerializer();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSharedCors();
    builder.Services.AddSwaggerGen();
}

static void ConfigureApplication(WebApplication app)
{
    app.UseHttpsRedirection();
    app.UseSharedCors();
    app.MapControllers();
    app.UseSharedSwagger();
}