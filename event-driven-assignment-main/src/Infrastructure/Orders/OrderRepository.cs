namespace Infrastructure.Orders;

/// <inheritdoc/>
public sealed class OrderRepository(ApplicationDbContext applicationDbContext) : IOrderRepository
{
    private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;
    public IUnitOfWork UnitOfWork => _applicationDbContext;

    /// <inheritdoc/>
    public async Task<Order> AddAsync(Order order)
    {
        _applicationDbContext.Add(order);
        await _applicationDbContext.SaveChangesAsync();
        return order;
    }
}