using Core.EventStore;
using DriverService.AppCore.Domain;
using MediatR;
using Services;

namespace DriverService.AppCore.UseCases.Cdc;

public sealed class DriverCdcConsumer(IEventStoreService eventStoreService, IEventBusService eventBusService)
    : INotificationHandler<DriverCreatedIntegrationEvent>
{
    public async Task Handle(DriverCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("Driver Created");
        DriverInfo driverInfo = new(Guid.Parse(notification.Id), notification.FullName, notification.Email, notification.PhoneNumber);
        await eventStoreService.ApplyDomainEvents(driverInfo);
        driverInfo.DomainEvents.ToList()
            .ForEach(async e => await eventBusService.PublishEventAsync((dynamic)e, cancellationToken));
        
    }
}