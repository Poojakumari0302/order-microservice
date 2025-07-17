using MediatR;
using Domain.Shared.Entities;

namespace Infrastructure
{
    static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, ApplicationDbContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<DomainEntity>()
                .Where(
                    x => x.Entity.DomainEvents != null && 
                    x.Entity.DomainEvents.Count != 0
                );

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}