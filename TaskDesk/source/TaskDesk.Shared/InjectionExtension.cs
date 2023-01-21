using System.Reflection;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TaskDesk.Domain;
using TaskDesk.Shared.Behaviours;
using TaskDesk.Shared.Exceptions.Extensions;

namespace TaskDesk.Shared;

public static class InjectionExtension
{
    public static void AddSharedDependencies(this IServiceCollection services)
    {
        var aa = GetAutoMapperProfilesFromAllAssemblies();

        services.AddAutoMapper(
            (serviceProvider, autoMapper) =>
            {
                autoMapper.AddCollectionMappers();
                autoMapper.UseEntityFrameworkCoreModel<EntitiesDbContext>(serviceProvider);
            }, GetAutoMapperProfilesFromAllAssemblies());

        services.AddValidatorsFromAssemblies(GetSolutionAssemblies());
        services.AddMediatR(GetSolutionAssemblies());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }

    public static void AddSharedSerializer(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ExceptionHandlerAttribute>();
        })
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            options.SerializerSettings.ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() };
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
        });
    }

    public static void AddSharedCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                });
        });
    }

    public static void UseSharedCors(this WebApplication app)
    {
        app.UseCors();
    }

    public static void AddSharedSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Task Desk API",
                Version = "v1",
                Description = "Task Desk Endpoints",
                Contact = new OpenApiContact
                {
                    Name = "Andrey Ku"
                },
            });

            options.CustomSchemaIds(type => type.ToString());

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });
    }

    public static void UseSharedSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }

    public static void SetSameSite(HttpContext httpContext, CookieOptions options)
    {
        if (options.SameSite == SameSiteMode.None)
        {
            options.SameSite = SameSiteMode.Unspecified;
        }
    }

    public static Assembly[] GetSolutionAssemblies()
        => AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName?.Contains("Desk") ?? false).ToArray();

    private static IEnumerable<Type> GetAutoMapperProfilesFromAllAssemblies()
    {
        return from assembly in AppDomain.CurrentDomain.GetAssemblies()
               from aType in assembly.GetTypes()
               where aType.IsClass && !aType.IsAbstract && aType.IsSubclassOf(typeof(Profile))
               select aType;
    }
}
