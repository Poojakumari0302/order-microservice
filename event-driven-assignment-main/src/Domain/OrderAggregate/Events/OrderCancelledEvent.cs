using Domain.Shared.Events;

namespace Domain.OrderAggregate.Events;

public sealed class OrderCancelledEvent : IEvent
{
    public long OrderId { get; }
    public DateTime CanceledAtUtc { get; }

    public OrderCanceledEvent(long orderId, DateTime canceledAtUtc)
    {
        OrderId = orderId;
        CanceledAtUtc = canceledAtUtc;
    }
}