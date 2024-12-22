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
                t.AddSagaStateMachine<BookingStateMachine, BookingState, BookingDefinition>();
                t.UsingKafka((context, configurator) =>
                {
                    configurator.Host(configuration.GetValue<string>("Kafka:BootstrapServers"));
                });
            });
        });
        action?.Invoke(services);
        return services;
    }
}