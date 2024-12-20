using Core.Domain;

namespace TripService.AppCore.Domain;

public class Location : BaseEntity
{
    public string LocationName { get; set; }
    public decimal Lat { get; set; }
    public decimal Lng { get; set; }
    public Guid BookingId { get; set; }
    public virtual Booking Booking { get; set; }
}