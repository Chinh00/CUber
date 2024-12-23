using DriverService.AppCore.Domain.Outboxs;
using Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace DriverService.Infrastructure.Data;

public class DriverContext(DbContextOptions<DriverContext> options) : AppBaseContext(options)
{
    public DbSet<VehicleOutbox> VehicleOutboxes { get; set; }
}