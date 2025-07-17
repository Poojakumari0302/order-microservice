using Domain.Shared.Events;

namespace Domain.OrderAggregate.Events;

public sealed class OrderRegisteredEvent : IEvent
{
    public string OrderNumber { get; set; }
}