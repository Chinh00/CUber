using Contracts.Services;
using MediatR;

namespace DriverService.AppCore.UseCases.Masstransits;

public class DriverCreatedDomainEventConsumer : INotificationHandler<DriverCreatedDomainEvent>
{
    public Task Handle(DriverCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}