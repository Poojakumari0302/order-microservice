using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Domain.Shared.Settings;

namespace Infrastructure.Shared.Idempotency;

public sealed class RequestManager(IDistributedCache distributedCache, ILogger<RequestManager> logger, IOptions<CacheSettings> cacheSettings) : IRequestManager
{
    private readonly ILogger<RequestManager> _logger = logger;
    private readonly IDistributedCache _distributedCache = distributedCache;
    private readonly CacheSettings _cacheSettings = cacheSettings?.Value;

    public async Task<bool> MessageExistAsync(Guid messageId)
    {
        _logger.LogInformation("Checking if messageId={messageId} exists in the cache.", messageId);

        var cacheKey = $"{_cacheSettings.KeyName}-{messageId.ToString()}";
        var existingRequest = await _distributedCache.GetStringAsync(cacheKey);

        if (!string.IsNullOrEmpty(existingRequest))
            return true;

        var cacheExpirationInMinutes = _cacheSettings.CacheExpirationInMinutes < 5 
            ? 5 
            : _cacheSettings.CacheExpirationInMinutes;
        var cacheEntryOptions = new DistributedCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(cacheExpirationInMinutes));

        await _distributedCache.SetStringAsync(cacheKey, DateTime.UtcNow.ToString(), cacheEntryOptions);
        return false;
    }

    public async Task RemoveMessageAsync(Guid messageId)
    {
        _logger.LogInformation("Removing messageId={messageId} from the cache.", messageId);

        var cacheKey = $"{_cacheSettings.KeyName}-{messageId.ToString()}";
        await _distributedCache.RemoveAsync(cacheKey);
    }
}