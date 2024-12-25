using Core.Domain;

namespace Contracts.Services;

public class TripPickedIntegrationEvent : IIntegrationEvent
{
    public Guid TripId { get; set; }
}