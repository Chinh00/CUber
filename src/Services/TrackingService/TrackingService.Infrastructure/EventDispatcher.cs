using Core.Domain;
using MassTransit;
using MediatR;

namespace TrackingService.Infrastructure;

public class EventDispatcher(IMediator mediator) : IConsumer<IIntegrationEvent>
{

    public async Task Consume(ConsumeContext<IIntegrationEvent> context)
    {
        Console.WriteLine($"Event: {context.Message.GetType().FullName}");
        await mediator.Publish(context.Message);
    }
}