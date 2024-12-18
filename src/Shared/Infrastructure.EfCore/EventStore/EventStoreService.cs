using Core.EventStore;

namespace Infrastructure.EfCore.EventStore;

public class EventStoreService(EventStoreContext context) : IEventStoreService
{
    public async Task ApplyDomainEvents<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : IAggregateRoot
    {
        foreach (var domainEvent in aggregateRoot.DomainEvents)
        {
            var @event = EventStoreEntity.Create<TAggregateRoot>(Guid.NewGuid(), domainEvent, domainEvent.Version);
            await context.Set<EventStoreEntity>().AddAsync(@event);
        }
    }

    public async Task<IEnumerator<DomainEvent>> LoadEventsAsync(Guid aggregateId)
    {
        throw new NotImplementedException();
    }
}