using Api.Application.Behaviours;
using MediatR;

namespace Api.Application.Commands;

public class CancelOrderCommand() : BaseApplicationRequest, IRequest<Domain.OrderAggregate.Order>
{
    
    public long OrderId { get; set; }
    public Guid CorrelationId { get; set; }

}