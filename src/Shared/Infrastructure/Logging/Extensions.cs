using Serilog;

namespace Infrastructure.Logging;

public static class Extensions
{
    public static IServiceCollection AddLoggingService(this IServiceCollection services,
        Action<IServiceCollection>? action = null)
    {
        var loggerConfiguration = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        
        Log.Logger = loggerConfiguration;
        services.AddLogging();
        action?.Invoke(services);
        return services;
    }
}