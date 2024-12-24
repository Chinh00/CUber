using Core.EventStore;
using Infrastructure.Masstransit.BusService;
using MassTransit;

namespace UserService.Infrastructure.Masstransit;

public static class Extensions
{
    public static IServiceCollection AddMasstransitService(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        services.AddMassTransit(k =>
        {
            k.SetKebabCaseEndpointNameFormatter();
            k.UsingInMemory();
            k.AddRider(t =>
            {
                // t.AddProducer<CustomerCreatedDomainEvent>(nameof(CustomerCreatedDomainEvent));
                // t.AddProducer<CustomerUpdatedDomainEvent>(nameof(CustomerUpdatedDomainEvent));




                t.AddConsumer<EventDispatcher>();
                t.UsingKafka((context, configurator) =>
                {
                    configurator.Host(configuration["Kafka:BootstrapServers"]);
                    // configurator.TopicEndpoint<CustomerCreatedDomainEvent>(nameof(CustomerCreatedDomainEvent), "customer-group",
                    //     c =>
                    //     {
                    //         c.AutoOffsetReset = AutoOffsetReset.Earliest;
                    //         c.CreateIfMissing(n => n.NumPartitions = 1);
                    //         c.ConfigureConsumer<EventDispatcher>(context);
                    //     });
                    // configurator.TopicEndpoint<CustomerUpdatedDomainEvent>(nameof(CustomerUpdatedDomainEvent), "customer-group",
                    //     c =>
                    //     {
                    //         c.AutoOffsetReset = AutoOffsetReset.Earliest;
                    //         c.CreateIfMissing(n => n.NumPartitions = 1);
                    //         c.ConfigureConsumer<EventDispatcher>(context);
                    //     });
                });
            });
        });
        services.AddScoped<IEventBusService, BusService>();
        action?.Invoke(services);
        return services;
    }   
}