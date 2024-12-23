using Core.Domain;

namespace DriverService.AppCore.Domain;

public class Vehicle : AggregateBase
{
    public string NumberId { get; set; }
    public string VehicleName { get; private set; }
    public VehicleType VehicleType { get; private set; }
    
}