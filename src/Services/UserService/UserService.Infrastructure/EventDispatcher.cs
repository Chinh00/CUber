using MassTransit;
using MediatR;

namespace UserService.Infrastructure;

public sealed class EventDispatcher(ILogger<EventDispatcher> logger) : IConsumer<INotification>
{
    public async Task Consume(ConsumeContext<INotification> context)
    {
        logger.LogInformation("EventDispatcher received notification {@context}", context.Message);
    }
}