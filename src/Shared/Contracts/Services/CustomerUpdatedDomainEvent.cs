using Core.Domain;

namespace Contracts.Services;

public record CustomerUpdatedDomainEvent(Guid Id, string FullName, string Email, string PhoneNumber,long Version) : DomainEvent;