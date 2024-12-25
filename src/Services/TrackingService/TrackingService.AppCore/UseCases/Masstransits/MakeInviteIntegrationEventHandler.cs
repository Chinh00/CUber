using Contracts.Services;
using Infrastructure.Redis;
using MassTransit;
using MediatR;
using TrackingService.AppCore.Domain;

namespace TrackingService.AppCore.UseCases.Masstransits;

public class MakeInviteIntegrationEventHandler(
    ILogger<MakeInviteIntegrationEventHandler> logger,
    IRedisService<Location> locationService,
    IRedisService<TripInfo> tripInfoService,
    ITopicProducer<DriverInviteIntegrationEvent> driverInviteEventProducer,
    ITopicProducer<DriverNotfoundIntegrationEvent> driverNotFoundEventProducer)
    : INotificationHandler<MakeInvitedIntegrationEvent>
{
    public async Task Handle(MakeInvitedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling MakeInvitedIntegrationEvent");
         var tripInfo = new TripInfo()
         {
             Id = notification.TripId,
             Locations = notification.Locations
         };
         var driverIds = await locationService.HashGetKeysAsync(nameof(Location), cancellationToken);
         driverIds.Select(c => Guid.Parse(c.Split(":")[1])).ToList().ForEach(
             async void (e) =>
                 await tripInfoService.HashOrSetAsync(nameof(TripInfo), $"{e}:{tripInfo.Id}", tripInfo, cancellationToken));
         await driverInviteEventProducer.Produce(new { notification.TripId}, cancellationToken);
    }
    
}