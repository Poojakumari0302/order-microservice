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
}
