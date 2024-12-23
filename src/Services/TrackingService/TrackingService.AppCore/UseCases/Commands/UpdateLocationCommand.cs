using AutoMapper;
using Core.Domain;
using Infrastructure.Redis;
using MediatR;
using TrackingService.AppCore.Domain;
using TrackingService.AppCore.Dtos;

namespace TrackingService.AppCore.UseCases.Commands;

public record UpdateLocationCommand(Guid VehicleId) : ICommand<LocationDto>
{
    internal class Handler(IRedisService<Location> locationService, IMapper mapper)
        : IRequestHandler<UpdateLocationCommand, ResultModel<LocationDto>>
    {
        public async Task<ResultModel<LocationDto>> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await locationService.HashGetAsync(nameof(Location),
                $"{nameof(Location)}:{request.VehicleId.ToString()}");
            return ResultModel<LocationDto>.Create(mapper.Map<LocationDto>(location));
        }
    }
}