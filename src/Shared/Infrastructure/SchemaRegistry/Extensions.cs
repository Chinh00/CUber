using Avro.Generic;
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

    public static async Task<byte[]> AsByteArray<TData>(this TData record, ISchemaRegistryClient registry, string topicName)
        where TData : ISpecificRecord
    {
        var avroDeserializeConfig = new AvroSerializerConfig()
        {
            SubjectNameStrategy = SubjectNameStrategy.Topic
        };
        var deserizalize = new AvroSerializer<TData>(registry, avroDeserializeConfig);
        return await deserizalize.SerializeAsync(record, new SerializationContext(MessageComponentType.Value, topicName));
    }
    public static async Task<byte[]> AsBytes<TData>(this TData record, ISchemaRegistryClient registry, string topicName)
        where TData : GenericRecord
    {
        var avroDeserializeConfig = new AvroSerializerConfig()
        {
            SubjectNameStrategy = SubjectNameStrategy.Topic
        };
        var deserizalize = new AvroSerializer<TData>(registry, avroDeserializeConfig);
        return await deserizalize.SerializeAsync(record, new SerializationContext(MessageComponentType.Value, topicName));
    }
    public static async Task<ISpecificRecord?> AsRecord<TConvert>(this byte[] payload, ISchemaRegistryClient schemaRegistryClient)
        where TConvert : ISpecificRecord
    {
        ISpecificRecord record = null;
        var deserialize = new AvroDeserializer<TConvert>(schemaRegistryClient);
        record = await deserialize.DeserializeAsync(payload, false, new SerializationContext());
        return record;
    }
}