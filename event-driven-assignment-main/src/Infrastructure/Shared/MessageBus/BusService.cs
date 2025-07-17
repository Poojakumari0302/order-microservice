using MassTransit;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Shared.MessageBus;

public class BusService(IBusControl busControl) : IHostedService
{
    private readonly IBusControl _busControl = busControl;

    public Task StartAsync(CancellationToken cancellationToken) =>
        _busControl.StartAsync(cancellationToken);

    public Task StopAsync(CancellationToken cancellationToken) =>
        _busControl.StopAsync(cancellationToken);
}