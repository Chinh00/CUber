namespace Core.Domain;

public interface IEvent
{
    
}

public interface DomainEvent : INotification, IEvent
{
    long Version { get; }
}

public interface IIntegrationEvent : IEvent {}