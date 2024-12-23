using AutoMapper;
using Core.Domain;
using Core.EventStore;
using DriverService.AppCore.Domain;
using DriverService.AppCore.UseCases.Dtos;
using MediatR;

namespace DriverService.AppCore.UseCases.Commands;

public record DriverAddVehicleCommand(Guid DriverId, string NumberId, string VehicleName) : ICommand<DriverInfoDto>
{
    internal class Handler(IEventStoreService eventStore, IMapper mapper, IEventBusService eventBusService)
        : IRequestHandler<DriverAddVehicleCommand, ResultModel<DriverInfoDto>>
    {
        public async Task<ResultModel<DriverInfoDto>> Handle(DriverAddVehicleCommand request, CancellationToken cancellationToken)
        {
            var (_, numberId, vehicleName) = request;
            var driverInfo = await eventStore.LoadEventsAsync<DriverInfo>(request.DriverId, cancellationToken);
            driverInfo.AddVehicle(new Vehicle(numberId, vehicleName));
            await eventStore.ApplyDomainEvents(driverInfo);
            driverInfo.DomainEvents.ToList().ForEach(async e =>
                await eventBusService.PublishEventAsync((dynamic)e, cancellationToken));
            return ResultModel<DriverInfoDto>.Create(mapper.Map<DriverInfoDto>(driverInfo));
        }
    }
}