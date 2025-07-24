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

        /// Loads an Order aggregate from the data store.
        Task<Order?> GetByIdAsync(long orderId);
        
        /// Persists changes to the order, including any new domain events
        Task SaveAsync(Order order);
    }
}