namespace Core.EventStore;

public interface IEventBusService : IDomainService
{
    Task PublishEventAsync<TAggregate>(TAggregate aggregate, CancellationToken cancellationToken = default) where TAggregate : AggregateBase;
}