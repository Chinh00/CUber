using Core.Domain;

namespace Contracts.Services;

public class PaymentFailIntegrationEvent : IIntegrationEvent
{
    public Guid TripId { get; set; }
}