using Domain.Shared.Exceptions;

namespace Domain.OrderAggregate.ValueObjects
{
    /// <summary>
    /// Represents an order status in the domain.
    /// </summary>
public enum OrderStatus
{
    Created,
    Processing,
    Shipped,
    Canceled
}
}