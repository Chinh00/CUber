using Infrastructure.AutoMapper;
using Infrastructure.EfCore.Data;
using UserService.Infrastructure.Configs;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddDataStore(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {
        services.AddEfCoreDefault<UserContext>(configuration, typeof(UserContext));
        services.AddHostedService<UserMigrationHostedService>();
        services.AddAutoMapperService(typeof(DriverMapperConfig));

        action?.Invoke(services);
        return services;
    }
}