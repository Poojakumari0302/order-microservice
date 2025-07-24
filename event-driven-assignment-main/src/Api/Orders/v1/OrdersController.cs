using System.Threading.Tasks;
using Api.Application.Commands;
using Api.Configuration.Controllers.v1;
using Domain.OrderAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Orders.v1;

public class OrdersController(
    ILogger<OrdersController> logger, 
    IMediator mediator) : BaseController<OrdersController>(logger, mediator)
{
    [HttpPost]
    [ProducesResponseType(typeof(Order), 201)]
    public async Task<IActionResult> PostAsync([FromBody]RegisterOrderCommand createOrderCommand)
    {
        await Mediatr.Send(createOrderCommand);

        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(typeof(Order), 201)]
    public async Task<IActionResult> PostAsync([FromBody]CancelOrderCommand cancelOrderCommand)
    {
        await Mediatr.Send(cancelOrderCommand);

        return Ok();
    }
}