using App.Metrics;
using App.Metrics.Counter;

namespace Api.Configuration.Metrics;

internal static class MetricsRegistry
{
    public static CounterOptions NewDemoCreated => new CounterOptions
    {
        Name = "New Demo Counter",
        MeasurementUnit = Unit.Calls
    };
}