using Core.Domain;

namespace TripService.AppCore.Domain;

public class Booking : AggregateBase
{
    public Guid CustomerId { get; private set; }
    public virtual CustomerInfo CustomerInfo { get; private set; }
    public decimal Price { get; private set; }
    public decimal? Tip { get; private set; }
    public DateTime BookingDate { get; private set; } 
    public string Notes { get; private set; }
    public virtual ICollection<Location> Locations { get; private set; }
}