using Core.Domain;

namespace Contracts.Services;

public record CustomerCreatedDomainEvent(string FullName, string Email, string PhoneNumber,long Version) : DomainEvent;
