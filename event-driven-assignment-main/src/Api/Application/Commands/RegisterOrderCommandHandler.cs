using Domain.OrderAggregate;
using Domain.OrderAggregate.Events;
using MassTransit;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Application.Commands;

public sealed class RegisterOrderCommandHandler(
    IOrderRepository orderRepository,
    ITopicProducer<OrderRegisteredEvent> producer) : IRequestHandler<RegisterOrderCommand, Order>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly ITopicProducer<OrderRegisteredEvent> _producer = producer;

    public async Task<Order> Handle(RegisterOrderCommand request, CancellationToken cancellationToken)
    {
        var (order, events) = Order.RegisterNewOrder(request.OrderNumber);

        var newOrder = await _orderRepository.AddAsync(order);

        foreach (var @event in events)
        {
            await _producer.Produce(@event, cancellationToken);
        }

        return newOrder;
    }
}