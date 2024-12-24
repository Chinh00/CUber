using Core.Domain;

namespace TrackingService.AppCore.Domain;

public class Location : BaseEntity
{
    public VehicleInfo Vehicle { get; set; }
    public decimal Longitude { get; set; }
    public decimal Latitude { get; set; }
    public decimal MinLongitude { get; set; }
    public decimal MinLatitude { get; set; }
    public decimal MaxLongitude { get; set; }
    public decimal MaxLatitude { get; set; }
}