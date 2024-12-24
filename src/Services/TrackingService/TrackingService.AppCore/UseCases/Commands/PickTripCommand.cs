using Core.Domain;
using Infrastructure.Redis;
using MediatR;
using TrackingService.AppCore.Domain;

namespace TrackingService.AppCore.UseCases.Commands;

public record PickTripCommand(Guid VehicleId, Guid TripId) : ICommand<bool>
{
    internal class Handler(IRedisService<TripInfo> redisService) : IRequestHandler<PickTripCommand, ResultModel<bool>>
    {

        public async Task<ResultModel<bool>> Handle(PickTripCommand request, CancellationToken cancellationToken)
        {
            var (vehicleId, tripId) = request;
            var keys = await redisService.HashGetKeysAsync(nameof(TripInfo), cancellationToken);
            keys = keys.Where(c => c.Contains(tripId.ToString()) && !c.Contains(vehicleId.ToString())).ToArray();
            keys.ToList().ForEach(async e =>
                await redisService.HashRemoveAsync(nameof(TripInfo), e, cancellationToken));
            return ResultModel<bool>.Create(true);
        }
    }
}