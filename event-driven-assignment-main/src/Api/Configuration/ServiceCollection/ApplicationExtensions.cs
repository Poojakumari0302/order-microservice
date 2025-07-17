#pragma warning disable 1591

using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Api.Application.Behaviours;

namespace Api.Configuration.ServiceCollection;

public static class ApplicationExtensions
{
    public static void AddApplicationDependencies(this IServiceCollection services)
    {
    }

    public static void AddApplicationPipelineBehaviors(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
    }
}

#pragma warning restore 1591