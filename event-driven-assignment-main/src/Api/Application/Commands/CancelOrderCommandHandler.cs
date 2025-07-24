using Domain.OrderAggregate;
using Domain.OrderAggregate.Events;
using MassTransit;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Application.Commands;
public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, Order>
{
    private readonly IOrderRepository _repository;
    private readonly IClock _clock;
    private readonly ITopicProducer<OrderCanceledEvent> _producer;

    public CancelOrderCommandHandler(
        IOrderRepository repository,
        IClock clock,
        ITopicProducer<OrderCanceledEvent> producer) ;
    {
        _repository = repository;
        _clock = clock;
        _producer = producer;
    }

    public async Task HandleAsync(CancelOrderCommand command)
    {
        var order = await _repository.GetByIdAsync(command.OrderId);
        if (order == null)
            throw new NotFoundException("Order not found");

        order.Cancel(_clock.UtcNow);
        await _repository.SaveAsync(order);

        await _producer.Produce(new
        {
            OrderId = order.Id,
            CorrelationId = command.CorrelationId,
            CanceledAtUtc = _clock.UtcNow
        });
    }
}