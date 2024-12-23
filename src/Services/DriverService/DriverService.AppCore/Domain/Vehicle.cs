using Core.Domain;

namespace DriverService.AppCore.Domain;

public class Vehicle : AggregateBase
{
    public Vehicle()
    {
        
    }

    public Vehicle(string numberId, string vehicleName)
    {
        NumberId = numberId;
        VehicleName = vehicleName;
    }
    
    public string NumberId { get; set; }
    public string VehicleName { get; private set; }
    public VehicleType VehicleType { get; private set; } = VehicleType.Motorcycle;

}