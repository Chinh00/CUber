using Core.Domain;

namespace TripService.AppCore.Domain;

public class CustomerInfo : BaseEntity
{
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public virtual ICollection<Booking> Bookings { get; set; }
}