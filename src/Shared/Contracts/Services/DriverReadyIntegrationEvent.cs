using Core.Domain;

namespace Contracts.Services;

public class DriverReadyIntegrationEvent : IIntegrationEvent
{
    public Guid TripId { get; set; }
}
