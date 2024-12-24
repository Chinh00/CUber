using Core.Domain;
using Services;

namespace TrackingService.AppCore.Domain;

public class TripInfo : BaseEntity
{
    public List<LocationDetail> Locations { get; set; } = [];
}