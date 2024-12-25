using Core.Domain;

namespace Contracts.Services;

public class PaymentSuccessIntegrationEvent : IIntegrationEvent
{
    public Guid TripId { get; set; }
}