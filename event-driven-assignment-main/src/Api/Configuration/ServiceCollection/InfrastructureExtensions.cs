using Domain.OrderAggregate;
using Domain.Shared.SeedWork;
using Infrastructure.HttpClients;
using Infrastructure.Orders;
using Infrastructure.Shared.Idempotency;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Net.Http;

namespace Api.Configuration.ServiceCollection;

public static class InfrastructureExtensions
{
    public static void AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IRequestManager, RequestManager>();
    }

    private static Action<IServiceProvider, HttpClient> ConfigureClient(string configSection)
    {
        return (provider, client) =>
        {
            try
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var options = configuration.GetSection(configSection).Get<HttpClientOptions>();
                client.BaseAddress = new Uri(options.BaseUrl);
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, "Client Exception");
            }
        };
    }
}