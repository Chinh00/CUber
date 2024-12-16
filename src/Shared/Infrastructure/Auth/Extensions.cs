namespace Infrastructure.Auth;

public static class Extensions
{
    private const string Cors = "Cors";
    public static IServiceCollection AddAuthService(this IServiceCollection services,
        Action<IServiceCollection>? action = null)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(Cors, builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        
        
        action?.Invoke(services);
        return services;
    }
    
    public static IApplicationBuilder UseAuthService(this WebApplication app)
    {
        app.UseCors(Cors);
        return app;
    }
}