using Contracts.Services;
using Core.Domain;

namespace UserService.AppCore.Domain;

public class Driver : AggregateBase
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public void Create(string fullName, string email, string phoneNumber)
    {
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        AddDomainEvent((version) => new DriverCreatedDomainEvent(Id, fullName, email, phoneNumber, version + 1));
    }
    public void ChangeActive()
    {
        IsActive = true;
        AddDomainEvent((version) => new DriverChangedActiveDomainEvent(Id, version + 1));
    }
    public void ChangeInActive()
    {
        IsActive = false;
        AddDomainEvent((version) => new DriverChangedInActiveDomainEvent(Id, version + 1));
    }

    protected override void ApplyDomainEvent(DomainEvent domainEvent)
    {
        base.ApplyDomainEvent(domainEvent);
        Apply((dynamic)domainEvent);
    }

    void Apply(DriverCreatedDomainEvent @event)
    {
        FullName = @event.FullName;
        Email = @event.Email;
        PhoneNumber = @event.PhoneNumber;
    }

    void Apply(DriverChangedActiveDomainEvent @event) => IsActive = true;
    void Apply(DriverChangedInActiveDomainEvent @event) => IsActive = false;

}