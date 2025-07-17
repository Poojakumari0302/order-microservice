using System;
using System.Collections;
using Domain.Shared.Enums;
using Domain.Shared.Extensions;

namespace Domain.Shared.Exceptions;

public abstract class BaseException : Exception
{
    protected BaseException(ErrorCodes errorCodes, string message, IDictionary data)
        : base(message)
    {
        Category = errorCodes.GetDescription();
        Data = data;
    }

    protected BaseException(ErrorCodes errorCodes, string message, IDictionary data, Exception innerException)
        : base(message, innerException)
    {
        Category = errorCodes.GetDescription();
        Data = data;
    }

    public override IDictionary Data { get; }
    public string Category { get; }
}