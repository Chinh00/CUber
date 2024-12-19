using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain;

public interface IAggregateRoot
{
    IReadOnlyCollection<DomainEvent> DomainEvents { get; }    
}
public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class AggregateBase : BaseEntity, IAggregateRoot
{
    [NotMapped]
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    
    [NotMapped]
    private IList<DomainEvent> _domainEvents;
    
    public long Version { get; set; } 
    
    
    
    public void AddDomainEvent(Func<long, DomainEvent> handler)
    {
        _domainEvents ??= new List<DomainEvent>();
        var domainEvent = handler(Version);    
        _domainEvents.Add(domainEvent);
    }

    protected virtual void ApplyDomainEvent(DomainEvent domainEvent)
    {
        Version = domainEvent.Version;
    } 
    
    public void LoadFromHistory(IEnumerable<DomainEvent> history)
    {
        foreach (var domainEvent in history)
        {
            ApplyDomainEvent(domainEvent);
        }
    }
    
}

public record EventStoreBase(Guid Id, DateTime CreatedAt);
