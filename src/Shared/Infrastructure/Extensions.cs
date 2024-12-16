using Infrastructure.Auth;
using Infrastructure.Controllers;
using Infrastructure.Logging;

namespace Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddServiceDefault(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        services.AddLoggingService();
        services.AddAuthService();
        services.AddControllerService();
        action?.Invoke(services);
        return services;
    }
    public static IApplicationBuilder UseServiceDefault(this WebApplication app)
    {
        app.UseAuthService();
        app.UseControllerService();
        
        return app;
    }
}