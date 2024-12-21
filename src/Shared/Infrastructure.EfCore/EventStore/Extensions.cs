using Core.EventStore;

namespace Infrastructure.EfCore.EventStore;

public static class Extensions
{
    public static IServiceCollection AddEventStore(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {
        services.AddDbContext<EventStoreContext>(c =>
        {
            c.UseNpgsql(configuration.GetConnectionString("Db"), builder =>
            {
                builder.EnableRetryOnFailure();
            }).UseSnakeCaseNamingConvention();
        });
        services.AddHostedService<EventStoreHostedService>();
        services.AddScoped<IEventStoreService, EventStoreService>();
        
        action?.Invoke(services);
        return services;
    }
}