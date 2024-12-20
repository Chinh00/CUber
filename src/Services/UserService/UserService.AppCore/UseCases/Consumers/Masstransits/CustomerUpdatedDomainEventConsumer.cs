using Contracts.Services;
using Infrastructure.Mongodb;
using MediatR;

namespace UserService.AppCore.UseCases.Consumers.Masstransits;

public sealed class CustomerUpdatedDomainEventConsumer(IMongoRepository<Projections.CustomerDetail> repository)
    : INotificationHandler<CustomerUpdatedDomainEvent>
{
    public async Task Handle(CustomerUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var (id, fullName, email, phoneNumber, version) = notification;
        await repository.OnReplaceAsync(
            new Projections.CustomerDetail(fullName, email, phoneNumber) { Id = id, Version = version },
            detail => detail.Id == notification.Id && detail.Version < notification.Version, cancellationToken);
    }
}