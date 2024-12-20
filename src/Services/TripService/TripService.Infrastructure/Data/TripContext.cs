using Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;
using TripService.AppCore.Domain;

namespace TripService.Infrastructure.Data;

public class TripContext(DbContextOptions<TripContext> options) : AppBaseContext(options)
{
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<CustomerInfo> CustomerInfos { get; set; }
    public DbSet<Location> Locations { get; set; }
}