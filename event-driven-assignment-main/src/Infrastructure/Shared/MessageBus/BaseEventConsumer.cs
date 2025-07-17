using MassTransit;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Shared.MessageBus
{
    public abstract class BaseEventConsumer<TConsumer, TEvent>(
        ILogger<TConsumer> logger,
        IRequestManager requestManager) :
            IEventConsumer<TConsumer, TEvent>
                where TConsumer : class
                where TEvent : class
    {
        private readonly ILogger<TConsumer> _logger = logger;
        private readonly IRequestManager _requestManager = requestManager;

        public ILogger<TConsumer> Logger => _logger;
        public IRequestManager RequestManager => _requestManager;

        public abstract Task Consume(ConsumeContext<TEvent> context);

        public async Task<bool> MessageHasBeenConsumedAsync(Guid messageId)
        {
            var messageAlreadyExists = await _requestManager.MessageExistAsync(messageId);

            if (messageAlreadyExists)
                _logger.LogWarning("The event messageId={messageId} is a duplicate, and has been processed.", messageId);

            return messageAlreadyExists;
        }
    }
}