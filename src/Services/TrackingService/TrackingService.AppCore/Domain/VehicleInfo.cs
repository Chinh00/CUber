using Core.Domain;

namespace TrackingService.AppCore.Domain;

public class VehicleInfo : BaseEntity
{
    public string VehicleName { get; set; }
    public VehicleStatus VehicleStatus { get; set; }
}