using Core.Domain;
using Services;

namespace Contracts.Services;

public class MakeInvitedIntegrationEvent : IIntegrationEvent
{
    public Guid TripId { get; set; }
    public List<LocationDetail> Locations { get; set; } = [];
};