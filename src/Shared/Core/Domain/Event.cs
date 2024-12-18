namespace Core.Domain;

public interface DomainEvent
{
    long Version { get; }
}