using Avro.Specific;
using Confluent.SchemaRegistry;
using Core.Domain;
using Core.Outbox;
using Core.Repository;
using Infrastructure.SchemaRegistry;

namespace Infrastructure.OutboxHandler;

public class OutboxHandler<TOutbox>(ISchemaRegistryClient schemaRegistryClient, IRepository<TOutbox> repository)
    where TOutbox : OutboxEntity

{

    public async ValueTask SendToOutboxAsync<TAggregate, TEvent>(
        TAggregate aggregateRoot,
        Func<(TOutbox, TEvent, string)> eventFunc, CancellationToken cancellationToken = default)    
        where TEvent : ISpecificRecord
        where TAggregate : AggregateBase
    {
        var (outbox, @event, topicName) = eventFunc();
        outbox.Id = Guid.NewGuid();
        outbox.AggregateType = typeof(TAggregate).Name;
        outbox.AggregateId = aggregateRoot.Id.ToString();
        outbox.Type = @event.GetType().Name;
        outbox.Payload = await @event.AsByteArray(schemaRegistryClient, topicName);
        await repository.AddAsync(outbox, cancellationToken);
    }
    
}