using Core.Domain;

namespace Services;

public partial class CustomerCreatedIntegrationEvent : IIntegrationEvent;
public partial class DriverCreatedIntegrationEvent : IIntegrationEvent;
public partial class VehicleCreatedIntegrationEvent : IIntegrationEvent;

public record LocationDetail(string LocationName, decimal Latitude, decimal Longitude);
