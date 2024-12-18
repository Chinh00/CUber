namespace Infrastructure.Swagger;

public static class Extensions
{
    public static IServiceCollection AddSwaggerService(this IServiceCollection services,
        Action<IServiceCollection>? action = null)
    {
        services.AddSwaggerGen();
        action?.Invoke(services);
        return services;
    }

    public static IApplicationBuilder UseSwaggerService(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }
}