using Core.Domain;
using Services;

namespace Contracts.Services;

public interface TripCreatedIntegrationEvent : IIntegrationEvent {
    public Guid TripId { get; set; }
    public DateTime BookingDate { get; set; }
    public List<LocationDetail> LocationDetails { get; set; }
}