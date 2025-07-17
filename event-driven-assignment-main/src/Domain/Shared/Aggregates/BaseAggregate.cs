using System;
using System.Threading.Tasks;
using Domain.Shared.SeedWork;

namespace Domain.Shared.Aggregates
{
    public abstract class BaseAggregate
    {
        private readonly IRequestManager _requestManager;

        protected BaseAggregate(IRequestManager requestManager) =>
            _requestManager = requestManager;

        protected async Task<bool> HandleIdempotency(Guid id) =>
            await _requestManager.MessageExistAsync(id);
    }
}