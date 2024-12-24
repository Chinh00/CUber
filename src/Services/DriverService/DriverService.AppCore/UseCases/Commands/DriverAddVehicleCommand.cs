using AutoMapper;
using Confluent.SchemaRegistry;
using Core.Domain;
using Core.EventStore;
using Core.Repository;
using DriverService.AppCore.Domain;
using DriverService.AppCore.Domain.Outboxs;
using DriverService.AppCore.UseCases.Dtos;
using Infrastructure.OutboxHandler;
using MediatR;
using Services;

namespace DriverService.AppCore.UseCases.Commands;

public record DriverAddVehicleCommand(Guid DriverId, string NumberId, string VehicleName) : ICommand<DriverInfoDto>
{
    internal class Handler(
        IEventStoreService eventStore,
        IMapper mapper,
        IEventBusService eventBusService,
        ISchemaRegistryClient schemaRegistryClient,
        IRepository<VehicleOutbox> repository)
        : OutboxHandler<VehicleOutbox>(schemaRegistryClient, repository), IRequestHandler<DriverAddVehicleCommand, ResultModel<DriverInfoDto>>
    {
        public async Task<ResultModel<DriverInfoDto>> Handle(DriverAddVehicleCommand request, CancellationToken cancellationToken)
        {
            var (_, numberId, vehicleName) = request;
            var driverInfo = await eventStore.LoadEventsAsync<DriverInfo>(request.DriverId, cancellationToken);
            var vehicle = new Vehicle(numberId, vehicleName);
            driverInfo.AddVehicle(vehicle);
            await eventStore.ApplyDomainEvents(driverInfo);
            driverInfo.DomainEvents.ToList().ForEach(async e =>
                await eventBusService.PublishEventAsync((dynamic)e, cancellationToken));
            var vehicleCreatedIntegrationEvent = new VehicleCreatedIntegrationEvent()
            {
                Id = vehicle.Id.ToString(),
                Name = vehicle.VehicleName
            };
            await SendToOutboxAsync(driverInfo, () =>
                (new VehicleOutbox(), vehicleCreatedIntegrationEvent, "vehicle_cdc_events"), cancellationToken);
            
            return ResultModel<DriverInfoDto>.Create(mapper.Map<DriverInfoDto>(driverInfo));
        }
    }
}