using AutoMapper;
using Core.Domain;
using Infrastructure.Redis;
using MediatR;
using TrackingService.AppCore.Domain;
using TrackingService.AppCore.UseCases.Dtos;

namespace TrackingService.AppCore.UseCases.Commands;

public record UpdateLocationCommand(Guid VehicleId, decimal Latitude, decimal Longitude) : ICommand<LocationDto>
{
    internal class Handler(IRedisService<Location> locationService, IMapper mapper)
        : IRequestHandler<UpdateLocationCommand, ResultModel<LocationDto>>
    {
        public async Task<ResultModel<LocationDto>> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await locationService.HashGetAsync(nameof(Location),
                $"{nameof(Location)}:{request.VehicleId.ToString()}");
            location.Latitude = request.Latitude;
            location.Longitude = request.Longitude;
            await locationService.HashSetAsync(nameof(Location),
                $"{nameof(Location)}:{request.VehicleId.ToString()}", location, cancellationToken);
            return ResultModel<LocationDto>.Create(mapper.Map<LocationDto>(location));
        }
    }
}