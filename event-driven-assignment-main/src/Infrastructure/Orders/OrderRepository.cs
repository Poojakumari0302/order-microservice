using Microsoft.EntityFrameworkCore;
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

    public async Task<Order?> GetByIdAsync(Guid orderId)
    {
        return await _applicationDbContext.Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task SaveAsync(Order order)
    {
        // Check if it's an update or new insert
        var existing = await _applicationDbContext.Orders.FindAsync(order.Id);
        if (existing == null)
        {
            _applicationDbContext.Orders.Add(order);
        }
        else
        {
            _applicationDbContext.Entry(existing).CurrentValues.SetValues(order);
        }

        await _applicationDbContext.SaveChangesAsync();
    }
}