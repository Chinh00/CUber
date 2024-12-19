using Core.Domain;

namespace Contracts.Services;

public record CustomerCreatedDomainEvent(Guid Id, string FullName, string Email, string PhoneNumber,long Version) : DomainEvent;
