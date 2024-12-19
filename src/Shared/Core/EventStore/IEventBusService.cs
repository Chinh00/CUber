namespace Core.EventStore;

public interface IEventBusService : IDomainService
{
    Task PublishEventAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : class, DomainEvent;
}