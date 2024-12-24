using Core.Domain;

namespace Contracts.Services;

public record DriverInviteIntegrationEvent(Guid TripId) : IIntegrationEvent
{
    
}