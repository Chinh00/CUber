using Core.Domain;

namespace Contracts.Services;

public record DriverAddedVehicleDomainEvent(Guid Id, Guid VehicleId, long Version) : DomainEvent;
