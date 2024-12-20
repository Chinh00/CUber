using Core.Domain;

namespace Contracts.Services;

public record DriverCreatedDomainEvent(Guid Id, string FullName, string Email, string PhoneNumber, long Version) : DomainEvent;