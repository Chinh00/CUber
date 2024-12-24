using Contracts.Services;
using Infrastructure.Mongodb;
using MediatR;

namespace DriverService.AppCore.UseCases.Masstransits;

public class DriverAddedVehicleDomainEventConsumer(
    IMongoRepository<Projections.DriverDetail> repository)
    : INotificationHandler<DriverAddedVehicleDomainEvent>
{
    public async Task Handle(DriverAddedVehicleDomainEvent notification, CancellationToken cancellationToken)
    {
        var driverDetail = await repository.FindOneAsync(e => e.Id == notification.Id, cancellationToken);
        var (id, vehicleId, vehicleName, numberId, vehicleType, version) = notification;
        driverDetail = driverDetail with
        {
            VehicleDetail = new Projections.VehicleDetail(vehicleId, vehicleName, numberId, vehicleType),
            Version = version
        };
        await repository.OnReplaceAsync(driverDetail, e => e.Id == notification.Id && e.Version < notification.Version, cancellationToken);
        
        
    }
}