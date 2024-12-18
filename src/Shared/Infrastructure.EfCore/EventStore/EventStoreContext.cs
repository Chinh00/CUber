using System.Text.Json;
using Core.EventStore;

namespace Infrastructure.EfCore.EventStore;

public class EventStoreContext(DbContextOptions<EventStoreContext> options) : DbContext(options)
{
    public DbSet<EventStoreEntity> EventStores { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<EventStoreEntity>(c =>
        {
            c.Property(e => e.Payload).HasConversion(l => JsonSerializer.Serialize(l, new JsonSerializerOptions()),
                r => JsonSerializer.Deserialize<DomainEvent>(r, new JsonSerializerOptions()));
        });
    }
}