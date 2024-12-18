namespace Core.EventStore;

public interface IEventStoreService
{
    Task ApplyDomainEvents<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : IAggregateRoot;
    Task<IEnumerator<DomainEvent>> LoadEventsAsync(Guid aggregateId);
}