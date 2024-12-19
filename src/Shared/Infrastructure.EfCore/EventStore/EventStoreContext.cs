using Core.EventStore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Infrastructure.EfCore.EventStore;

public class EventStoreContext(DbContextOptions<EventStoreContext> options) : DbContext(options)
{
    public DbSet<EventStoreEntity> EventStores { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<EventStoreEntity>(c =>
        {
            c.Property(e => e.Payload).HasConversion(new ValueConverter<DomainEvent,string>(l => JsonConvert.SerializeObject(l),
                r => JsonConvert.DeserializeObject<DomainEvent>(r)));
        });
    }
}