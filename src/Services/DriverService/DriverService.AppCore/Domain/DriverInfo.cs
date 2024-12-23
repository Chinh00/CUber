using Contracts.Services;
using Core.Domain;
using Core.Exception;

namespace DriverService.AppCore.Domain;

public class DriverInfo : AggregateBase
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public Guid? VehicleId { get; set; }
    public virtual Vehicle Vehicle { get; set; }


    public DriverInfo()
    {
        
    }

    public DriverInfo(Guid id, string fullName, string email, string phoneNumber)
    {
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        AddDomainEvent(version => new DriverCreatedDomainEvent(Id, FullName, null, phoneNumber, version + 1));
    }
    public void AddVehicle(Vehicle vehicle)
    {
        if (VehicleId is not null) throw new DomainException("Cannot add vehicle to vehicle details");
        VehicleId = vehicle.Id;
        AddDomainEvent(version => new DriverAddedVehicleDomainEvent(Id, vehicle!.Id, vehicle?.VehicleName,
            vehicle?.NumberId, vehicle?.VehicleType.ToString(), version + 1));
    }

    protected override void ApplyDomainEvent(DomainEvent domainEvent)
    {
        base.ApplyDomainEvent(domainEvent);
        Apply((dynamic)domainEvent);
    }

    void Apply(DriverAddedVehicleDomainEvent @event)
    {
        Id = @event.Id;
        VehicleId = @event.VehicleId;
        Vehicle = new Vehicle(@event.NumberId, @event.VehicleName);
    }
    void Apply(DriverCreatedDomainEvent @event)
    {
        Id = @event.Id;
        FullName = @event.FullName;
        Email = @event.Email;
        PhoneNumber = @event.PhoneNumber;
    }
}