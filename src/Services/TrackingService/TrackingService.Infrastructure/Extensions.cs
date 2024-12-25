using Confluent.Kafka;
using Contracts.Services;
using Infrastructure.OutboxHandler;
using Infrastructure.SchemaRegistry;
using MassTransit;
using Services;
using TrackingService.Infrastructure.Cdc;

namespace TrackingService.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddMasstransitService(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        services.AddMassTransit(c =>
        {
            c.SetKebabCaseEndpointNameFormatter();
            c.UsingInMemory();
            c.AddRider(t =>
            {
                t.AddProducer<DriverInviteIntegrationEvent>(nameof(DriverInviteIntegrationEvent));
                t.AddProducer<DriverNotfoundIntegrationEvent>(nameof(DriverNotfoundIntegrationEvent));
                t.AddProducer<TripPickedIntegrationEvent>(nameof(TripPickedIntegrationEvent));
                t.AddProducer<DriverReadyIntegrationEvent>(nameof(DriverReadyIntegrationEvent));
                t.AddProducer<TripEndIntegrationEvent>(nameof(TripEndIntegrationEvent));

                t.AddProducer<PaymentSuccessIntegrationEvent>(nameof(PaymentSuccessIntegrationEvent));
                t.AddProducer<PaymentFailIntegrationEvent>(nameof(PaymentFailIntegrationEvent));

                
                t.AddConsumer<EventDispatcher>();
                t.UsingKafka((context, config) =>
                {
                    config.Host(configuration.GetValue<string>("Kafka:BootstrapServers"));
                    config.TopicEndpoint<MakeInvitedIntegrationEvent>(nameof(MakeInvitedIntegrationEvent), "make-invite",
                        e =>
                        {
                            e.AutoOffsetReset = AutoOffsetReset.Earliest;
                            e.CreateIfMissing(n => n.NumPartitions = 1);
                            e.ConfigureConsumer<EventDispatcher>(context);
                        });
                });
            });
        });
        
        action?.Invoke(services);
        return services;
    }
    public static IServiceCollection AddCdcConsumers(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {
        services.AddBackgroundConsumer<VehicleCdcConsumerConfig>(c =>
        {
            c.Topic = "vehicle_cdc_events";
            c.GroupId = "vehicle_cdc_events_group";
            c.HandlePayload = async (client, s, arg3) =>
            {
                return await (s switch
                {
                    nameof(VehicleCreatedIntegrationEvent) => arg3.AsRecord<VehicleCreatedIntegrationEvent>(client),
                    _ => throw new ArgumentOutOfRangeException(nameof(s), s, null)
                });
            };
        });
        
        
        
        action?.Invoke(services);
        return services;
    }
}