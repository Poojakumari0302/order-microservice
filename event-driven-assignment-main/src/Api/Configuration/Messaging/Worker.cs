using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Api.Configuration.Messaging;

public class Worker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Do work here

            await Task.Delay(1000, stoppingToken);
        }
    }
}