using Infrastructure.OutboxHandler;
using Infrastructure.SchemaRegistry;
using Services;
using TripService.Infrastructure.Cdc;

namespace TripService.Infrastructure;

public static class Extensions
{
   
    public static IServiceCollection AddCdcConsumer(this IServiceCollection services,
        Action<IServiceCollection>? action = null)
    {

        services.AddBackgroundConsumer<CustomerConsumerConfig>(c =>
        {
            c.Topic = "customer_cdc_event";
            c.GroupId = "customer_cdc_event_group";
            c.HandlePayload = async (client, s, bytes) =>
            {
                return s switch
                {
                    nameof(CustomerCreatedIntegrationEvent) => await bytes.AsRecord<CustomerCreatedIntegrationEvent>(client),
                    _ => throw new ArgumentOutOfRangeException(nameof(s), s, null)
                };
            };
        });
        action?.Invoke(services);
        return services;
    }
}