using Microsoft.Extensions.DependencyInjection;

namespace Api.Configuration.ServiceCollection;

/// <summary>
///     MassTransit communication setup extension methods.
/// </summary>
public static class MassTransitExtensions
{
    /// <summary>
    ///     Add rabbitmq configuration for MassTransit.
    ///     <para>This is for development use ONLY.</para>
    /// </summary>
    public static IServiceCollection AddMassTransit(this IServiceCollection services, string host)
    {
        // if (!isIntegrationTest)
        // {
        //     services.AddMassTransit(serviceCollectionConfigurator =>
        //     {
        //         serviceCollectionConfigurator.AddConsumer<DriverAtStoreIntegrationEventConsumer>();
        //         serviceCollectionConfigurator.AddConsumer<DeliveryStatusChangedIntegrationEventConsumer>();
        //         serviceCollectionConfigurator.AddConsumer<OrderCancelledIntegrationEventConsumer>();
        //     });
        // }

        // services.AddScoped(provider => provider.GetRequiredService<IBus>().CreateRequestClient<Events.DriverAtStoreIntegrationEvent>());
        // services.AddScoped(provider => provider.GetRequiredService<IBus>().CreateRequestClient<Events.StoreCreatedIntegrationEvent>());
        // services.AddScoped(provider => provider.GetRequiredService<IBus>().CreateRequestClient<Events.StoreUpdatedIntegrationEvent>());
        // services.AddScoped(provider => provider.GetRequiredService<IBus>().CreateRequestClient<Events.StoreKitchenTimesUpdatedIntegrationEvent>());
        // services.AddScoped(provider => provider.GetRequiredService<IBus>().CreateRequestClient<Events.StoreRemovedIntegrationEvent>());
        // services.AddScoped(provider => provider.GetRequiredService<IBus>().CreateRequestClient<Events.NewOrderIsReadyIntegrationEvent>());
        // services.AddScoped(provider => provider.GetRequiredService<IBus>().CreateRequestClient<Events.OrderUpdatedIntegrationEvent>());
        // services.AddScoped(provider => provider.GetRequiredService<IBus>().CreateRequestClient<Events.StoreAddressUpdatedIntegrationEvent>());

        // return services.AddSingleton(serviceProvider => Bus.Factory.CreateUsingRabbitMq(busConfigurator =>
        // {
        //     busConfigurator.Host(host, "/", h => { });
        //     // busConfigurator.SetLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>());
            
        //     // Setup event publisher
        //     busConfigurator.ConfigurePublisher<Events.StoreCreatedIntegrationEvent>();
        //     busConfigurator.ConfigurePublisher<Events.StoreUpdatedIntegrationEvent>();
        //     busConfigurator.ConfigurePublisher<Events.StoreKitchenTimesUpdatedIntegrationEvent>();
        //     busConfigurator.ConfigurePublisher<Events.StoreRemovedIntegrationEvent>();
        //     busConfigurator.ConfigurePublisher<Events.NewOrderIsReadyIntegrationEvent>();
        //     busConfigurator.ConfigurePublisher<Events.OrderUpdatedIntegrationEvent>();
        //     busConfigurator.ConfigurePublisher<Events.StoreAddressUpdatedIntegrationEvent>();

        //     // Setup event consumer
        //     busConfigurator.ConfigureConsumer<DriverAtStoreIntegrationEventConsumer>(serviceProvider: serviceProvider, receiveEndpoint: "driver-at-store-queue");
        //     busConfigurator.ConfigureConsumer<DeliveryStatusChangedIntegrationEventConsumer>(serviceProvider: serviceProvider, receiveEndpoint: "delivery-status-changed-queue");
        //     busConfigurator.ConfigureConsumer<OrderCancelledIntegrationEventConsumer>(serviceProvider: serviceProvider, receiveEndpoint: "order-cancelled-queue");
        // }));
        return services;
    }
}