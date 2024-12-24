using Core.Domain;

namespace Contracts.Services;

public record DriverReadyIntegrationEvent(Guid TripId) : IIntegrationEvent;
