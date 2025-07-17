using Domain.Shared.Exceptions;

namespace Domain.OrderAggregate.ValueObjects
{
    /// <summary>
    /// Represents an order number in the domain.
    /// </summary>
    public sealed class OrderNumber
    {
        /// <summary>
        /// Gets the value of the order number.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderNumber"/> class.
        /// </summary>
        /// <param name="value">The value of the order number.</param>
        /// <exception cref="DomainException">Thrown when the order number is null or whitespace.</exception>
        public OrderNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException("Order number cannot be empty", "CorrelationId");
            }
            Value = value;
        }
    }
}