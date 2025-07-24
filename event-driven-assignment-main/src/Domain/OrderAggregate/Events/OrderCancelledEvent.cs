using Domain.Shared.Events;
using System;

namespace Domain.OrderAggregate.Events;

public sealed class OrderCancelledEvent : IEvent
{
    public Guid OrderId { get; }
    public DateTime CanceledAtUtc { get; }

    public OrderCancelledEvent(Guid orderId, DateTime canceledAtUtc)
    {
        OrderId = orderId;
        CanceledAtUtc = canceledAtUtc;
    }
}