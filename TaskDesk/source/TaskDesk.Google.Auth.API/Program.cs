using TaskDesk.Domain;
using TaskDesk.Identity;
using TaskDesk.Identity.Options;
using TaskDesk.Migrations;
using TaskDesk.Shared;

var builder = WebApplication.CreateBuilder(args);

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

    var googleOptions = builder.Configuration.GetSection("Auth:Google").Get<GoogleOptions>()!;

    builder.Services
        .AddAuthentication(options =>
        {
            options.DefaultSignInScheme = "External";
        }
        )
        .AddCookie("External")
        .AddGoogle(options =>
        {
            options.ClientId = googleOptions.ClientId;
            options.ClientSecret = googleOptions.ClientSecret;
        });

    // https://devblogs.microsoft.com/dotnet/upcoming-samesite-cookie-changes-in-asp-net-and-asp-net-core/
    builder.Services.Configure<CookiePolicyOptions>(options =>
    {
        options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
        options.OnAppendCookie = cookieContext =>
            TaskDesk.Shared.InjectionExtension.SetSameSite(cookieContext.Context, cookieContext.CookieOptions);
        options.OnDeleteCookie = cookieContext =>
            TaskDesk.Shared.InjectionExtension.SetSameSite(cookieContext.Context, cookieContext.CookieOptions);
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
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