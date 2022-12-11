using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TaskDesk.Domain;

public static class InjectionExtension
{
    public static IServiceCollection AddDomainDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<EntitiesDbContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("Default"),
                    x => x.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)),
                ServiceLifetime.Transient);

        services.AddScoped(p =>
                p.GetRequiredService<IDbContextFactory<EntitiesDbContext>>()
                    .CreateDbContext());

        return services;
    }
}