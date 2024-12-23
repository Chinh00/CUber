using Contracts.Services;
using Infrastructure.Mongodb;
using MediatR;

namespace DriverService.AppCore.UseCases.Masstransits;

public class DriverCreatedDomainEventConsumer(IMongoRepository<Projections.DriverDetail> repository)
    : INotificationHandler<DriverCreatedDomainEvent>
{

    public async Task Handle(DriverCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await repository.OnReplaceAsync(new Projections.DriverDetail(notification.FullName, notification.Email, notification.PhoneNumber)
        {
            Id = Guid.Parse(notification.Id.ToString()),
            Version = notification.Version
        }, e => e.Id == notification.Id && notification.Version > e.Version, cancellationToken);
    }
}