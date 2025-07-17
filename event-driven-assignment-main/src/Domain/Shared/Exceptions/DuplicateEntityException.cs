using System;
using System.Collections.Generic;
using Domain.Shared.Enums;

namespace Domain.Shared.Exceptions
{
    public class DuplicateEntityException : BaseException
    {
        public DuplicateEntityException(string message, string correlationId, long entityId = 0, ErrorCodes errorCodes = ErrorCodes.DuplicateEntity)
            : base(errorCodes, message, new Dictionary<string, string>
            {
                { "entityId", $"{entityId}" },
                { "correlationId", $"{correlationId}" }
            })
        {
        }

        public DuplicateEntityException(string message, string correlationId, Exception innerException, long entityId = 0, ErrorCodes errorCodes = ErrorCodes.DuplicateEntity)
            : base(errorCodes, message, new Dictionary<string, string>
            {
                { "entityId", $"{entityId}" },
                { "correlationId", $"{correlationId}" }
            }, innerException)
        {
        }
    }
}