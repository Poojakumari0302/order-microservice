using System;
using MediatR;

namespace Api.Application.Behaviours
{
    public abstract class BaseApplicationRequest : IBaseRequest
    {
        public Guid CorrelationId { get; set; } = Guid.NewGuid();
    }
}