namespace Domain.OrderAggregate
{
    /// <summary>
    /// Interface for the order repository.
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Adds a new order asynchronously.
        /// </summary>
        /// <param name="order">The order to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added order.</returns>
        Task<Order> AddAsync(Order order);
    }
}