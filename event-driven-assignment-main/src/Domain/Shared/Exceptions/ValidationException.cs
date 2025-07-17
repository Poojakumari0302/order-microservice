using System;
using System.Collections.Generic;
using Domain.Shared.Enums;

namespace Domain.Shared.Exceptions
{
    public class ValidationException : BaseException
    {
        public ValidationException(string message, long orderId = 0, long storeId = 0, ErrorCodes errorCodes = ErrorCodes.Validation)
            : base(errorCodes, message, new Dictionary<string, string>
            {
                { "orderId", $"{orderId}" },
                { "storeId", $"{storeId}" }
            })
        {
        }

        public ValidationException(string message, Exception innerException, long orderId = 0, long storeId = 0, ErrorCodes errorCodes = ErrorCodes.Validation)
            : base(errorCodes, message, new Dictionary<string, string>
            {
                { "orderId", $"{orderId}" },
                { "storeId", $"{storeId}" }
            }, innerException)
        {
        }
    }
}