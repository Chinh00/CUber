using Core.Domain;

namespace Contracts.Services;

public record DriverCancelTripIntegrationEvent(Guid TripId) : IIntegrationEvent
{
    
}