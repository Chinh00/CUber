namespace Core.EventStore;

public record EventStoreEntity(Guid Id, string AggregateType, string EventType, DomainEvent Payload, long Version)
{ 
    public static EventStoreEntity Create<TAggregate>(Guid id, DomainEvent payload, long version) where TAggregate : IAggregateRoot => new EventStoreEntity(id, typeof(TAggregate).Name, nameof(payload), payload, version);
}