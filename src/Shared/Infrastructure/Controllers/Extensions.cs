namespace Infrastructure.Controllers;

public static class Extensions
{
    public static IServiceCollection AddControllerService(this IServiceCollection services, Type[] types,
        Action<IServiceCollection>? action = null)
    {
        services.AddControllers();
        //services.AddMediatR(e => e.RegisterServicesFromAssemblies());
        
        action?.Invoke(services);
        return services;
    }
    public static IApplicationBuilder UseControllerService(this WebApplication app)
    {
        app.MapControllers();
        return app;
    }
}