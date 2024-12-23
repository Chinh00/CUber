using Core.Domain;

namespace Contracts.Services;

public record DriverAddedVehicleDomainEvent(
    Guid Id,
    Guid VehicleId,
    string VehicleName,
    string NumberId,
    string VehicleType,
    long Version) : DomainEvent;
