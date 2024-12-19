using Core.Domain;
using Core.EventStore;
using MassTransit;

namespace Infrastructure.Masstransit.BusService;

public class BusService(IServiceProvider serviceProvider, ILogger<BusService> logger) : IEventBusService
{
    public async Task PublishEventAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : class, DomainEvent
    {
        var topicProducer = serviceProvider.GetRequiredService<ITopicProducer<TEvent>>();
        if (topicProducer is not null) await topicProducer.Produce(@event, cancellationToken);
    }
}