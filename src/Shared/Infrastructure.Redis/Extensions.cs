namespace Infrastructure.Redis;

public static class Extensions
{
    public static IServiceCollection AddRedisService(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {
        
        action?.Invoke(services);
        return services;
    }
}