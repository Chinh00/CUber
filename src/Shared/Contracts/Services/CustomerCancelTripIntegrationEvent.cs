using Core.Domain;

namespace Contracts.Services;

public class CustomerCancelTripIntegrationEvent : IIntegrationEvent
{
    public Guid TripId { get; set; }
}