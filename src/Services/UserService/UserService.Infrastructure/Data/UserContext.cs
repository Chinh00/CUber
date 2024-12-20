using Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;
using UserService.AppCore.Domain;
using UserService.AppCore.Domain.Outbox;

namespace UserService.Infrastructure.Data;

public class UserContext(DbContextOptions<UserContext> options) : AppBaseContext(options)
{
    public DbSet<CustomerOutbox> CustomerOutboxes { get; set; }
    public DbSet<DriverOutbox> DriverOutboxes { get; set; }

}