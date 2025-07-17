using Api.Application.Behaviours;
using MediatR;

namespace Api.Application.Commands;

public class RegisterOrderCommand() : BaseApplicationRequest, IRequest<Domain.OrderAggregate.Order>
{
    public string OrderNumber { get; set; }
}