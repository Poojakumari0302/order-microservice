using System;
using System.Collections.Generic;
using Domain.Shared.Enums;

namespace Domain.Shared.Exceptions;

public class DomainException : BaseException
{
    public DomainException(string message, string correlationId, ErrorCodes errorCodes = ErrorCodes.Domain)
        : base(errorCodes, message, new Dictionary<string, string>
        {
            { "correlationId", $"{correlationId}" }
        })
    {
    }

    public DomainException(string message, string correlationId, Exception innerException, ErrorCodes errorCodes = ErrorCodes.Domain)
        : base(errorCodes, message, new Dictionary<string, string>
        {
            { "correlationId", $"{correlationId}" }
        }, innerException)
    {
    }
}