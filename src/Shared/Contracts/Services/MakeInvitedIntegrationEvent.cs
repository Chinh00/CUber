using Core.Domain;
using MediatR;

namespace Contracts.Services;

public interface MakeInvitedIntegrationEvent : IIntegrationEvent, INotification
{
    public Guid TripId { get; set; }
};