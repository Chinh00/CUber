namespace Infrastructure.Redis;

public static class Extensions
{
    public static IServiceCollection AddRedisService(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {
        services.AddOptions<RedisOptions>().Bind(configuration.GetSection(RedisOptions.RedisName));
        services.AddSingleton(typeof(IRedisService<>), typeof(RedisService<>));
        
        action?.Invoke(services);
        return services;
    }
}