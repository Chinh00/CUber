using Core.Domain;

namespace Contracts.Services;

public class TripEndIntegrationEvent : IIntegrationEvent
{
    public Guid TripId { get; set; }
}