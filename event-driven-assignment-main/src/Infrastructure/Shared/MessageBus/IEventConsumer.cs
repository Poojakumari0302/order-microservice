using MassTransit;

namespace Infrastructure.Shared.MessageBus
{
    public interface IEventConsumer<TConsumer, TEvent> : IConsumer<TEvent> 
        where TConsumer : class
        where TEvent : class
    {
    }
}