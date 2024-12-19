namespace Core.Domain;

public class EventBase
{
    
}

public interface DomainEvent : INotification
{
    long Version { get; }
}