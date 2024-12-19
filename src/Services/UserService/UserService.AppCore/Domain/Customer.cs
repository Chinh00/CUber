using Contracts.Services;
using Core.Domain;

namespace UserService.AppCore.Domain;

public class Customer : AggregateBase
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }


    public void Create(string fullName, string email, string phoneNumber)
    {
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        AddDomainEvent((version) => new CustomerCreatedDomainEvent(Id, fullName, email, phoneNumber, version + 1));
    }
    public void Update(string fullName, string email, string phoneNumber)
    {
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        AddDomainEvent((version) => new CustomerUpdatedDomainEvent(Id, fullName, email, phoneNumber, version + 1));
    }

    protected override void ApplyDomainEvent(DomainEvent domainEvent)
    {
        base.ApplyDomainEvent(domainEvent);
        Apply((dynamic)domainEvent);
    }

    void Apply(CustomerCreatedDomainEvent @event)
    {
        Id = @event.Id;
        FullName = @event.FullName;
        Email = @event.Email;
        PhoneNumber = @event.PhoneNumber;
    }
    void Apply(CustomerUpdatedDomainEvent @event)
    {
        Id = @event.Id;
        FullName = @event.FullName;
        Email = @event.Email;
        PhoneNumber = @event.PhoneNumber;
    }

}