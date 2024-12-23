using AutoMapper;
using Core.Domain;
using Core.EventStore;
using DriverService.AppCore.Domain;
using DriverService.AppCore.UseCases.Dtos;
using MediatR;

namespace DriverService.AppCore.UseCases.Commands;

public record DriverAddVehicleCommand(Guid DriverId, Guid VehicleId) : ICommand<DriverInfoDto>
{
    internal class Handler(IEventStoreService eventStore, IMapper mapper)
        : IRequestHandler<DriverAddVehicleCommand, ResultModel<DriverInfoDto>>
    {
        public async Task<ResultModel<DriverInfoDto>> Handle(DriverAddVehicleCommand request, CancellationToken cancellationToken)
        {
            var driverInfo = await eventStore.LoadEventsAsync<DriverInfo>(request.DriverId, cancellationToken);
            driverInfo.AddVehicle(request.VehicleId);
            await eventStore.ApplyDomainEvents(driverInfo);
            return ResultModel<DriverInfoDto>.Create(mapper.Map<DriverInfoDto>(driverInfo));
        }
    }
}