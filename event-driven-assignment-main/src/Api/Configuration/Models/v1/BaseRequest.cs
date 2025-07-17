using System;

namespace Api.Configuration.Models.v1;

/// <summary>
///     Version 1 base request model.
/// </summary>
public abstract class BaseRequest
{
    private Guid _correlationId;

    /// <summary>
    ///     The correlation identifier.
    /// </summary>
    public Guid CorrelationId
    {
        get {
            if (_correlationId.Equals(Guid.Empty))
                return Guid.NewGuid();
            return _correlationId;
        }
        set {
            _correlationId = value;
        }
    }
}