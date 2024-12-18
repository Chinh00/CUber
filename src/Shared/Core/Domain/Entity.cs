using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain;

public interface IAggregateRoot
{
    IReadOnlyCollection<DomainEvent> DomainEvents { get; }    
}
public class BaseEntity
{
    public Guid Id { get; set; }
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
    
    
    public void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents ??= new List<DomainEvent>();
        _domainEvents.Add(domainEvent);
    }
    
}