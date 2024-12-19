using MassTransit;
using MediatR;

namespace UserService.Infrastructure;

public sealed class EventDispatcher(ILogger<EventDispatcher> logger, IMediator mediator) : IConsumer<INotification>
{
    public async Task Consume(ConsumeContext<INotification> context)
    {
        await mediator.Publish(context.Message);
    }
}