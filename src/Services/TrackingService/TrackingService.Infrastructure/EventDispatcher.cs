using Core.Domain;
using MassTransit;
using MediatR;

namespace TrackingService.Infrastructure;

public sealed class EventDispatcher(ILogger<EventDispatcher> logger, IServiceProvider provider) : IConsumer<IIntegrationEvent>
{
    public async Task Consume(ConsumeContext<IIntegrationEvent> context)
    {
        Console.WriteLine("EventDispatcher");
        var mediator = provider.GetRequiredService<IMediator>();
        await mediator.Publish((dynamic)context.Message);
    }
}