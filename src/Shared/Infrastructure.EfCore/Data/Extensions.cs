namespace Infrastructure.EfCore.Data;

public static class Extensions
{
    public static IServiceCollection AddEfCoreDefault<TDbContext>(this IServiceCollection services, IConfiguration configuration, Type type,
        Action<IServiceCollection>? action = null)
        where TDbContext : AppBaseContext
    {

        services.AddDbContext<TDbContext>(builder =>
        {
            builder.UseNpgsql(configuration.GetConnectionString("Db"), optionsBuilder =>
            {
                optionsBuilder.EnableRetryOnFailure();
            });
        });
        services.AddHostedService<MigrationHostedService<TDbContext>>();
        services.Scan(e =>
            e.FromAssembliesOf(type).AddClasses(t => t.AssignableTo<IRootRepository>()).AsImplementedInterfaces()
                .WithScopedLifetime());
        action?.Invoke(services);
        return services;
    }
}