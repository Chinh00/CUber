namespace Infrastructure.AutoMapper;

public static class Extensions
{
    public static IServiceCollection AddAutoMapperService(this IServiceCollection services, Type anchor,
        Action<IServiceCollection>? action = null)
    {
        services.AddAutoMapper(anchor.Assembly);
        action?.Invoke(services);
        return services;
    }
}