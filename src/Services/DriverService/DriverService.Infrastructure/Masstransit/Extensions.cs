using Confluent.Kafka;
using Contracts.Services;
using MassTransit;

namespace DriverService.Infrastructure.Masstransit;

public static class Extensions
{
    public static IServiceCollection AddMasstransitService(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {
        services.AddMassTransit(e =>
        {
            e.SetKebabCaseEndpointNameFormatter();
            e.UsingInMemory();
            e.AddRider(c =>
            {
                c.AddProducer<DriverCreatedDomainEvent>(nameof(DriverCreatedDomainEvent));
                c.AddProducer<DriverAddedVehicleDomainEvent>(nameof(DriverAddedVehicleDomainEvent));    
                
                
                c.AddConsumer<EventDispatcher>();
                c.UsingKafka((context, config) =>
                {
                    config.Host(configuration.GetValue<string>("Kafka:BootstrapServers"));
                    config.TopicEndpoint<DriverCreatedDomainEvent>(nameof(DriverCreatedDomainEvent), "driver-group",
                        t =>
                        {
                            t.CreateIfMissing(n => n.NumPartitions = 1);
                            t.AutoOffsetReset = AutoOffsetReset.Earliest;
                            t.ConfigureConsumer<EventDispatcher>(context);
                        });
                    config.TopicEndpoint<DriverAddedVehicleDomainEvent>(nameof(DriverAddedVehicleDomainEvent), "driver-group",
                        t =>
                        {
                            t.CreateIfMissing(n => n.NumPartitions = 1);
                            t.AutoOffsetReset = AutoOffsetReset.Earliest;
                            t.ConfigureConsumer<EventDispatcher>(context);
                        });
                });
            });
        });
        
        
        
        action?.Invoke(services);
        return services;
    }
}