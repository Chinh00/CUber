using MassTransit;

namespace NotificationService.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddMasstransitService(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {
        action?.Invoke(services);
        return services;
    }
}