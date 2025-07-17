using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Configuration.Controllers.v1;

/// <summary>
///     Base controller abstraction version 1.0
/// </summary>
/// <typeparam name="T"></typeparam>
/// <remarks>
///     Base controller constructor
/// </remarks>
/// <param name="logger">Injected logging object for current type.</param>
/// <param name="mediator">Application mediator object.</param>
[ApiController]
[ApiVersion("1.0")]
[Consumes("application/json")]
[Produces("application/json")]
[Route("v{version:apiVersion}/[controller]")]
public abstract class BaseController<T>(ILogger<T> logger, IMediator mediator) : ControllerBase where T : class
{
    private readonly ILogger<T> _logger = logger;
    private readonly IMediator _mediator = mediator;

    internal ILogger<T> Logger => _logger;
    internal IMediator Mediatr => _mediator;
}