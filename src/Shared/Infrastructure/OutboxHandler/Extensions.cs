using Confluent.Kafka;

namespace Infrastructure.OutboxHandler;

public static class Extensions
{
    public static IServiceCollection AddBackgroundConsumer<TConsumerConfig>(this IServiceCollection services,
        Action<TConsumerConfig> action)
        where TConsumerConfig : KafkaBackgroundConsumerConfig
    {
        services.AddOptions<TConsumerConfig>().BindConfiguration(KafkaBackgroundConsumerConfig.Name).Configure(action);
        services.AddHostedService<KafkaBackgroundConsumer<TConsumerConfig>>();
        return services;
    }
}