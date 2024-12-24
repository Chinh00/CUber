using Core.Domain;

namespace Contracts.Services;

public class DriverInviteIntegrationEvent : IIntegrationEvent
{
    public Guid TripId { get; set; }
}
