namespace Core.EventStore;

public record EventStoreEntity(Guid Id, Guid AggregateId, string AggregateType, string EventType, DomainEvent Payload, long Version, DateTime CreateAt)
{ 
    public static EventStoreEntity Create<TAggregate>(Guid id, Guid aggregateId, DomainEvent payload) where TAggregate : IAggregateRoot => new EventStoreEntity(id, aggregateId, typeof(TAggregate).Name, payload.GetType().Name, payload, payload.Version, DateTime.UtcNow);
}