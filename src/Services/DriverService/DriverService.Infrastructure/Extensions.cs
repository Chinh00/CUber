namespace DriverService.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddDataStore(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {
        action?.Invoke(services);
        return services;
    }
}