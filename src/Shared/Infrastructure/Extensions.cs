
using Infrastructure.Swagger;

namespace Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddServiceDefault(this IServiceCollection services, IConfiguration configuration, Type [] types,
        Action<IServiceCollection>? action = null)
    {

        services.AddLoggingService();
        services.AddAuthService();
        services.AddControllerService(types);
        services.AddSwaggerService();
        action?.Invoke(services);
        return services;
    }
    public static IApplicationBuilder UseServiceDefault(this WebApplication app)
    {
        app.UseAuthService();
        app.UseControllerService();
        app.UseSwaggerService();
        return app;
    }
}