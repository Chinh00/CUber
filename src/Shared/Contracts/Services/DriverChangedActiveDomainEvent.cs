using Core.Domain;

namespace Contracts.Services;

public record DriverChangedActiveDomainEvent(Guid Id, long Version) : DomainEvent;