﻿using Microsoft.Extensions.DependencyInjection;
using TaskDesk.Management.Blazor.Core.Endpoints.Project;

namespace TaskDesk.Management.Blazor.Core;

public static class InjectionExtension
{
    public static void AddBlazorCoreDependencies(this IServiceCollection services)
    {
        services.AddScoped<ProjectEndpoints>();
    }
}
