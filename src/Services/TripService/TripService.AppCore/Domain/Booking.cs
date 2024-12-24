using Core.Domain;

namespace TripService.AppCore.Domain;

public class Booking : AggregateBase
{
    public Guid CustomerId { get;  set; }
    public virtual CustomerInfo CustomerInfo { get;  set; }
    public decimal Price { get;  set; }
    public decimal? Tip { get;  set; }
    public DateTime BookingDate { get;  set; } 
    public string Notes { get;  set; }
    public virtual ICollection<Location> Locations { get;  set; } = new List<Location>();
}