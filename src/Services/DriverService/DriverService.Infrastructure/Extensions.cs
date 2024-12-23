using Core.EventStore;
using DriverService.Infrastructure.Cdc;
using DriverService.Infrastructure.Configs;
using DriverService.Infrastructure.Data;
using Infrastructure.AutoMapper;
using Infrastructure.EfCore.Data;
using Infrastructure.Masstransit.BusService;
using Infrastructure.OutboxHandler;
using Infrastructure.SchemaRegistry;
using Services;

namespace DriverService.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddDataStore(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {
        services.AddAutoMapperService(typeof(DriverMapperConfig));
        services.AddScoped<IEventBusService, BusService>();
        services.AddEfCoreDefault<DriverContext>(configuration, typeof(DriverContext));
        services.AddHostedService<DriverMigrationHostedService>();
        
        
        action?.Invoke(services);
        return services;
    }

    public static IServiceCollection AddCdcConsumers(this IServiceCollection services,
        Action<IServiceCollection>? action = null)
    {
        services.AddBackgroundConsumer<DriverConsumerConfig>(c =>
        {
            c.Topic = "driver_cdc_events";
            c.GroupId = "driver_cdc_events_group";
            c.HandlePayload = async (client, s, arg3) =>
            {
                return await (s switch
                {
                    nameof(DriverCreatedIntegrationEvent) => arg3.AsRecord<DriverCreatedIntegrationEvent>(client),
                    _ => throw new ArgumentOutOfRangeException(nameof(s), s, null)
                });
            };

        });
        action?.Invoke(services);
        return services;
    }
}