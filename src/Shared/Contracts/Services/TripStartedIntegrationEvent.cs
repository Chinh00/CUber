using Core.Domain;

namespace Contracts.Services;

public record TripStartedIntegrationEvent(Guid TripId) : IIntegrationEvent
{
    
}