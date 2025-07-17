using System;
using System.Threading.Tasks;

namespace Domain.Shared.SeedWork
{
    public interface IRequestManager
    {
        Task<bool> MessageExistAsync(Guid messageId);
        Task RemoveMessageAsync(Guid messageId);
    }
}