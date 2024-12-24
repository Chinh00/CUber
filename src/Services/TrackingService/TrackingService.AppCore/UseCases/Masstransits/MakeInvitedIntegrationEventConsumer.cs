using Contracts.Services;
using Infrastructure.Redis;
using MassTransit;
using MediatR;
using TrackingService.AppCore.Domain;

namespace TrackingService.AppCore.UseCases.Masstransits;

public class MakeInvitedIntegrationEventConsumer(
    IRedisService<TripInfo> tripService,
    IRedisService<Location> locationService,
    ITopicProducer<DriverInviteIntegrationEvent> producerInvite,
    ITopicProducer<DriverNotfoundIntegrationEvent> topicProducer)
    : INotificationHandler<MakeInvitedIntegrationEvent>
{
    public async Task Handle(MakeInvitedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("MakeInvitedIntegrationEventConsumer");
        var tripInfo = new TripInfo()
        {

        };
        //calculate distance
        var driverIds = await locationService.HashGetKeysAsync(nameof(Location), cancellationToken);
        driverIds.Select(Guid.Parse).ToList().ForEach(
            async void (e) =>
                await tripService.HashOrSetAsync(nameof(TripInfo), $"{e}:{tripInfo.Id}", tripInfo, cancellationToken));
        
    }
}