using Core.Domain;
using Infrastructure.Redis;
using MediatR;
using TrackingService.AppCore.Domain;

namespace TrackingService.AppCore.UseCases.Commands;

public record CreateSessionShiftCommand(Guid VehicleId) : ICommand<bool>
{
    internal class Handler(IRedisService<Location> locationService)
        : IRequestHandler<CreateSessionShiftCommand, ResultModel<bool>>
    {

        public async Task<ResultModel<bool>> Handle(CreateSessionShiftCommand request, CancellationToken cancellationToken)
        {
            var result = await locationService.HashSetAsync(nameof(Location), $"{nameof(Location)}:{request.VehicleId.ToString()}",
                new Location()
                {
                    VehicleId = request.VehicleId
                }, cancellationToken);
            return ResultModel<bool>.Create(true);
        }
    }
}