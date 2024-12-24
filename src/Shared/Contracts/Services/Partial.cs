using Core.Domain;
using MediatR;

namespace Services;

public partial class CustomerCreatedIntegrationEvent : IIntegrationEvent, INotification;
public partial class DriverCreatedIntegrationEvent : IIntegrationEvent, INotification;
public partial class VehicleCreatedIntegrationEvent : IIntegrationEvent, INotification;

public record LocationDetail(string LocationName, decimal Latitude, decimal Longitude);
