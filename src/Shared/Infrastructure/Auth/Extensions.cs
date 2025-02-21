using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Infrastructure.Auth;

public static class Extensions
{
    private const string Cors = "Cors";
    public static IServiceCollection AddAuthService(this IServiceCollection services, IConfiguration configuration,
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

        services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.Authority = configuration.GetValue<string>("IdentityServer:Url");
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters.ValidateIssuer = false;
            options.TokenValidationParameters.ValidateAudience = false;
            options.TokenValidationParameters.SignatureValidator = (token, parameters) => new JsonWebToken(token);
        });
        services.AddAuthorization();

        
        
        
        action?.Invoke(services);
        return services;
    }
    
    public static IApplicationBuilder UseAuthService(this WebApplication app)
    {
        app.UseCors(Cors);
        return app;
    }
}