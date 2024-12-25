using Confluent.Kafka;
using Contracts.Services;
using Infrastructure.Mongodb;
using MassTransit;
using TripService.Infrastructure.Masstransit.StateMachine;

namespace TripService.Infrastructure.Masstransit;

public static class Extensions
{
    public static IServiceCollection AddMasstransitService(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {
        services.AddMassTransit(r =>
        {
            r.SetKebabCaseEndpointNameFormatter();
            r.UsingInMemory();
            r.AddRider(t =>
            {
                t.AddProducer<TripCreatedIntegrationEvent>(nameof(TripCreatedIntegrationEvent));
                t.AddProducer<MakeInvitedIntegrationEvent>(nameof(MakeInvitedIntegrationEvent));

                
                
                var mOption = new MongodbOption();
                configuration.GetSection(MongodbOption.Mongodb).Bind(mOption);
                t.AddSagaStateMachine<BookingStateMachine, BookingState, BookingDefinition>().MongoDbRepository(c =>
                {
                    c.Connection = mOption.ToString();
                    c.DatabaseName = mOption.Database;
                    c.CollectionName = nameof(BookingStateMachine);
                });
                t.UsingKafka((context, config) =>
                {
                    config.Host(configuration.GetValue<string>("Kafka:BootstrapServers"));
                    config.TopicEndpoint<TripCreatedIntegrationEvent>(nameof(TripCreatedIntegrationEvent), "trip-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(n => n.NumPartitions = 1);
                            c.ConfigureSaga<BookingState>(context);
                        });
                    config.TopicEndpoint<MakeInvitedIntegrationEvent>(nameof(MakeInvitedIntegrationEvent), "trip-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(n => n.NumPartitions = 1);
                            c.ConfigureSaga<BookingState>(context);
                        });
                    config.TopicEndpoint<DriverInviteIntegrationEvent>(nameof(DriverInviteIntegrationEvent), "trip-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(n => n.NumPartitions = 1);
                            c.ConfigureSaga<BookingState>(context);
                        });
                    config.TopicEndpoint<DriverNotfoundIntegrationEvent>(nameof(DriverNotfoundIntegrationEvent), "trip-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(n => n.NumPartitions = 1);
                            c.ConfigureSaga<BookingState>(context);
                        });
                    config.TopicEndpoint<TripPickedIntegrationEvent>(nameof(TripPickedIntegrationEvent), "trip-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(n => n.NumPartitions = 1);
                            c.ConfigureSaga<BookingState>(context);
                        });
                    config.TopicEndpoint<DriverCancelTripIntegrationEvent>(nameof(DriverCancelTripIntegrationEvent), "trip-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(n => n.NumPartitions = 1);
                            c.ConfigureSaga<BookingState>(context);
                        });
                    config.TopicEndpoint<DriverReadyIntegrationEvent>(nameof(DriverReadyIntegrationEvent), "trip-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(n => n.NumPartitions = 1);
                            c.ConfigureSaga<BookingState>(context);
                        });
                    config.TopicEndpoint<TripEndIntegrationEvent>(nameof(TripEndIntegrationEvent), "trip-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(n => n.NumPartitions = 1);
                            c.ConfigureSaga<BookingState>(context);
                        });
                    config.TopicEndpoint<CustomerCancelTripIntegrationEvent>(nameof(CustomerCancelTripIntegrationEvent), "trip-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(n => n.NumPartitions = 1);
                            c.ConfigureSaga<BookingState>(context);
                        });
                    config.TopicEndpoint<PaymentSuccessIntegrationEvent>(nameof(PaymentSuccessIntegrationEvent), "trip-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(n => n.NumPartitions = 1);
                            c.ConfigureSaga<BookingState>(context);
                        });
                    config.TopicEndpoint<PaymentFailIntegrationEvent>(nameof(PaymentFailIntegrationEvent), "trip-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(n => n.NumPartitions = 1);
                            c.ConfigureSaga<BookingState>(context);
                        });

                });
            });
        });
        action?.Invoke(services);
        return services;
    }
}