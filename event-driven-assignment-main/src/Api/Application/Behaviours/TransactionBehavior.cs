using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Serilog;
using Newtonsoft.Json;

namespace Api.Application.Behaviours;

/// <summary>
/// Behavior for handling transactions in the request pipeline.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public class TransactionBehavior<TRequest, TResponse>(ApplicationDbContext dbContext,
       IDiagnosticContext diagnosticContext) : IPipelineBehavior<TRequest, TResponse> where TRequest : BaseApplicationRequest, IRequest<TResponse>
{
    private readonly ApplicationDbContext _dbContext = dbContext ?? throw new ArgumentException(nameof(ApplicationDbContext));
    private readonly IDiagnosticContext _diagnosticContext = diagnosticContext;

    /// <summary>
    /// Handles the request within a transaction.
    /// </summary>
    /// <param name="request">The request instance.</param>
    /// <param name="next">The next delegate in the pipeline.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The response from the next delegate in the pipeline.</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _diagnosticContext.Set("CommandInfo", new
        {
            request.CorrelationId,
            TypeName = request.GetType().Name,
            RawData = JsonConvert.SerializeObject(request)
        });
        var response = default(TResponse);

        if (_dbContext.HasActiveTransaction)
        {
            return await next();
        }

        var strategy = _dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _dbContext.BeginTransactionAsync();

            response = await next();
            await _dbContext.CommitTransactionAsync(transaction);
        });

        return response;
    }
}