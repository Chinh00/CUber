using Core.Domain;

namespace Contracts.Services;

public record TripCreatedIntegrationEvent(Guid TripId) : IIntegrationEvent;