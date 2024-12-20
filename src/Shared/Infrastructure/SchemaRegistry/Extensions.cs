using Avro.Specific;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;

namespace Infrastructure.SchemaRegistry;

public static class Extensions
{
    public static IServiceCollection AddSchemaRegistryService(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {
        services.AddScoped<ISchemaRegistryClient>(c => new CachedSchemaRegistryClient(new SchemaRegistryConfig()
        {
            Url = configuration["SchemaRegistry:Url"],
        }));
        action?.Invoke(services);
        return services;
    }

    public static async Task<byte[]> AsByteArray(this ISpecificRecord record, ISchemaRegistryClient registry, string topicName)
    {
        var avroDeserializeConfig = new AvroSerializerConfig()
        {
            SubjectNameStrategy = SubjectNameStrategy.Topic
        };
        var deserizalize = new AvroSerializer<ISpecificRecord>(registry, avroDeserializeConfig);
        return await deserizalize.SerializeAsync(record, new SerializationContext(MessageComponentType.Value, topicName));
    }
}