using Contracts.Services;
using Core.Domain;
using Infrastructure.Redis;
using MassTransit;
using MediatR;
using TrackingService.AppCore.Domain;

namespace TrackingService.AppCore.UseCases.Commands;

public record DriverEndCommand(Guid VehicleId, Guid TripId) : ICommand<bool>
{
    internal class Handler(IRedisService<TripInfo> redisService, ITopicProducer<TripEndIntegrationEvent> topicProducer)
        : IRequestHandler<DriverEndCommand, ResultModel<bool>>
    {
        public async Task<ResultModel<bool>> Handle(DriverEndCommand request, CancellationToken cancellationToken)
        {
            await redisService.HashRemoveAsync(nameof(TripInfo), $"{request.VehicleId}:{request.TripId}",
                cancellationToken);
            await topicProducer.Produce(new { request.TripId }, cancellationToken);
            return ResultModel<bool>.Create(true);
        }
    }
}