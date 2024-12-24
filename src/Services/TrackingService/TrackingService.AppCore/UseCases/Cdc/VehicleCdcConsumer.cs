using Infrastructure.Redis;
using MediatR;
using Services;
using TrackingService.AppCore.Domain;

namespace TrackingService.AppCore.UseCases.Cdc;

public class VehicleCdcConsumer(IRedisService<Location> cdcService)
    : INotificationHandler<VehicleCreatedIntegrationEvent>
{

    public async Task Handle(VehicleCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await cdcService.HashOrSetAsync(nameof(Location), $"{nameof(Location)}:{notification.Id.ToString()}",
            new Location()
            {
                Vehicle = new VehicleInfo()
                {
                    VehicleStatus = VehicleStatus.InActive,
                    Id = Guid.Parse(notification.Id),
                    VehicleName = notification.Name
                }
            }, cancellationToken);
    }
}