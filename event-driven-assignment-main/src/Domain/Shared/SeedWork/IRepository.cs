using Domain.Shared.Entities;

namespace Domain.Shared.SeedWork
{
    public interface IRepository<T> where T : DomainEntity
    {
        IUnitOfWork UnitOfWork { get; }
    }
}