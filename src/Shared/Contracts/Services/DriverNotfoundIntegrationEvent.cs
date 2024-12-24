using Core.Domain;

namespace Contracts.Services;

public record DriverNotfoundIntegrationEvent(Guid TripId) : IIntegrationEvent
{
    
}