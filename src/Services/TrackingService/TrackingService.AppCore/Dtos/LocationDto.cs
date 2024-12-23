using AutoMapper;
using TrackingService.AppCore.Domain;

namespace TrackingService.AppCore.Dtos;

public record LocationDto
{
    public Guid VehicleId { get; set; }
    public decimal Longitude { get; set; }
    public decimal Latitude { get; set; }
    public decimal MinLongitude { get; set; }
    public decimal MinLatitude { get; set; }
    public decimal MaxLongitude { get; set; }
    public decimal MaxLatitude { get; set; }
}

public class LocationMapperConfig : Profile
{
    public LocationMapperConfig()
    {
        CreateMap<Location, LocationDto>();
    }
}