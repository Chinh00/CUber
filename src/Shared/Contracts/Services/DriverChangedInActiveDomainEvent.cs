using Core.Domain;

namespace Contracts.Services;

public record DriverChangedInActiveDomainEvent(Guid Id, long Version) : DomainEvent;