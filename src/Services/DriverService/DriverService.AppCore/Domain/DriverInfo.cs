using Contracts.Services;
using Core.Domain;
using Core.Exception;

namespace DriverService.AppCore.Domain;

public class DriverInfo : AggregateBase
{
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }

    public Guid? VehicleId { get; set; }
    public virtual Vehicle Vehicle { get; set; }


    public DriverInfo()
    {
        
    }

    public DriverInfo(Guid id, string fullName, string phoneNumber)
    {
        FullName = fullName;
        AddDomainEvent(version => new DriverCreatedDomainEvent(Id, FullName, null, phoneNumber, version));
    }
    public void AddVehicle(Guid vehicleId)
    {
        if (VehicleId is not null) throw new DomainException("Cannot add vehicle to vehicle details");
        VehicleId = vehicleId;
        AddDomainEvent(version => new DriverAddedVehicleDomainEvent(Id, VehicleId.Value, version));
    }

    protected override void ApplyDomainEvent(DomainEvent domainEvent)
    {
        base.ApplyDomainEvent(domainEvent);
        Apply((dynamic)domainEvent);
    }

    void Apply(DriverAddedVehicleDomainEvent @event)
    {
        VehicleId = @event.VehicleId;
    }
    void Apply(DriverCreatedDomainEvent @event)
    {
        FullName = @event.FullName;
        PhoneNumber = @event.PhoneNumber;
    }
}