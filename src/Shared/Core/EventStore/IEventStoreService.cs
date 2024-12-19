namespace Core.EventStore;

public interface IEventStoreService
{
    Task ApplyDomainEvents<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateBase;
    Task<TEntity> LoadEventsAsync<TEntity> (Guid aggregateId, CancellationToken cancellationToken) where TEntity : AggregateBase, new();
}