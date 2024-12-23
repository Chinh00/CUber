namespace Infrastructure.SignaIR;

public static class Extensions
{
    public static IServiceCollection AddSocketService(this IServiceCollection services,
        Action<IServiceCollection>? action = null)
    {
        services.AddSignalR();
        action?.Invoke(services);
        return services;
    }

    public static IApplicationBuilder UseSocketService<THub>(this WebApplication app) where THub : HubBase
    {
        app.MapHub<THub>(HubBase.HubName);
        return app;
    }
}