using Core.EventStore;

namespace Infrastructure.EfCore.EventStore;

public sealed class EventStoreService(EventStoreContext context) : IEventStoreService
{
    public async Task ApplyDomainEvents<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateBase
    {
        foreach (var domainEvent in aggregateRoot.DomainEvents)
        {
            var @event = EventStoreEntity.Create<TAggregateRoot>(Guid.NewGuid(), aggregateRoot.Id, domainEvent);
            await context.Set<EventStoreEntity>().AddAsync(@event);
            await context.SaveChangesAsync();
        }
    }

    public async Task<TEntity> LoadEventsAsync<TEntity>(Guid aggregateId,
        CancellationToken cancellationToken)
        where TEntity : AggregateBase, new()

    {
        var eventStoreEntities = await context.Set<EventStoreEntity>().Where(e => e.AggregateId == aggregateId)
            .ToListAsync(cancellationToken: cancellationToken);
        
        var aggreagte = new TEntity();
        aggreagte.LoadFromHistory(eventStoreEntities.Select(e => e.Payload));
        return aggreagte;
    }

}