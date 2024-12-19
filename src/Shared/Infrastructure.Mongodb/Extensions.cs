using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Infrastructure.Mongodb;

public static class Extensions
{
    public static IServiceCollection AddMongodbService(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {
        services.AddOptions<MongodbOption>().Bind(configuration.GetSection(MongodbOption.Mongodb));
        services.AddScoped<IMongoDbContext, MongoDbContext>();
        services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        action?.Invoke(services);
        return services;
    }   
}