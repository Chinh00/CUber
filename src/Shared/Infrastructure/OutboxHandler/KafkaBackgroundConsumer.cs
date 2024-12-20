using Avro.Generic;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Infrastructure.SchemaRegistry;
using MediatR;
using Microsoft.Extensions.Options;

namespace Infrastructure.OutboxHandler;

public class KafkaBackgroundConsumer<TConfig> : BackgroundService 
    where TConfig : KafkaBackgroundConsumerConfig
{
    private readonly ILogger<KafkaBackgroundConsumer<TConfig>> _logger;
    private readonly KafkaBackgroundConsumerConfig _config;
    private readonly IServiceScopeFactory _scopeFactory;

    public KafkaBackgroundConsumer(ILogger<KafkaBackgroundConsumer<TConfig>> logger, IServiceScopeFactory scopeFactory, IOptions<TConfig> config)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        _config = config.Value;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    => Task.Factory.StartNew(() => KafkaConsumer(stoppingToken), stoppingToken, TaskCreationOptions.LongRunning, TaskScheduler.Current);

    private async Task KafkaConsumer(CancellationToken stoppingToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        using var schemaRegistryClient = new CachedSchemaRegistryClient(new SchemaRegistryConfig()
        {
            Url = _config.SchemaRegistryServer
        });
        var consumerBuilder = new ConsumerBuilder<string, GenericRecord>(_config)
            .SetErrorHandler((_, e) => _logger.LogError($"Error: {e.Reason}"))
            .SetStatisticsHandler((_, json) => _logger.LogInformation($"Statistics: {json}"))
            .SetValueDeserializer(new AvroDeserializer<GenericRecord>(schemaRegistryClient).AsSyncOverAsync())
            .Build();
        _logger.LogInformation(_config.Topic);
        consumerBuilder.Subscribe(_config.Topic);
        
        try
        {
            while (stoppingToken.IsCancellationRequested == false)
            {
                try
                {
                    var result = consumerBuilder.Consume();
                    if (result is null) continue;
                    var eventName = result.Message.Value.Schema?.Name;


                    var bytes = await result.Message.Value.AsBytes(schemaRegistryClient, $"{_config.Topic}-anchor");
                    var res = await _config.HandlePayload(schemaRegistryClient, eventName, bytes);

                    
                    if (res is INotification)
                    {
                        _logger.LogInformation("Kafka message received");
                        await mediator.Publish(res, stoppingToken);
                    }

                    consumerBuilder.Commit(result);
                }
                catch (ConsumeException e)
                {
                    _logger.LogInformation(e.Message);
                }
            }
        }
        catch (OperationCanceledException e)
        {
            _logger.LogError(e.Message);
            consumerBuilder.Close();
        }
        
    }
    
}