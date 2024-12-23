using MassTransit;
using MediatR;

namespace DriverService.Infrastructure;

public sealed class EventDispatcher(IMediator mediator) : IConsumer<INotification>
{
    public async Task Consume(ConsumeContext<INotification> context)
    {
        await mediator.Publish(context.Message);
    }
}