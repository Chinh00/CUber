using Core.Domain;
using Core.EventStore;
using MassTransit;

namespace Infrastructure.Masstransit.BusService;

public class BusService(IServiceProvider serviceProvider, ILogger<BusService> logger) : IEventBusService
{
    public async Task PublishEventAsync<TAggregate>(TAggregate aggregate, CancellationToken cancellationToken = default) where TAggregate : AggregateBase
    {
        foreach (var domainEvent in aggregate.DomainEvents)
        {
            var type = domainEvent.GetType();
            var topicProducer = serviceProvider.GetService(typeof(ITopicProducer<>).MakeGenericType(type));
            
        }
    }
}