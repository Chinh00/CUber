using MediatR;
using Services;

namespace DriverService.AppCore.UseCases.Cdc;

public sealed class DriverCdcConsumer : INotificationHandler<DriverCreatedIntegrationEvent>
{
    
    public Task Handle(DriverCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}