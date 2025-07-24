using System;
using System.Collections.Generic;
using Domain.OrderAggregate.Events;
using Domain.OrderAggregate.ValueObjects;
using Domain.Shared.Entities;
using Domain.Shared.Events;

namespace Domain.OrderAggregate;

public sealed class Order : DomainEntity
{
    public OrderNumber OrderNumber { get; init; }
    public long Id { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    public static (Order, IEnumerable<IEvent>) RegisterNewOrder(string orderNumber)
    {
        return (new Order
        {
            Id = Guid.NewGuid(),
            OrderNumber = new OrderNumber(orderNumber)
        }, [
                new OrderRegisteredEvent{ OrderNumber = orderNumber }
            ]);
    }

    private readonly List<IEvent> _domainEvents = new();

    public IReadOnlyCollection<IEvent> DomainEvents => _domainEvents.AsReadOnly();

    private Order() { }

    public Order(long id, DateTime createdAtUtc)
    {
        Id = id;
        CreatedAtUtc = createdAtUtc;
        Status = OrderStatus.Created;
    }
    
    public void Cancel(DateTime utcNow)
    {
        if (Status != OrderStatus.Created)
            throw new InvalidOperationException("Only newly created orders can be canceled.");

        if ((utcNow - CreatedAtUtc).TotalMinutes > 15)
            throw new InvalidOperationException("Cancel window has expired.");

        Status = OrderStatus.Canceled;
        _domainEvents.Add(new OrderCancelledEvent(Id, utcNow));
    }
}
