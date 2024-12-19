using Contracts.Services;
using Infrastructure.Mongodb;
using MediatR;

namespace UserService.AppCore.UseCases.Consumers.Masstransits;

public sealed class CustomerCreatedDomainEventConsumer(IMongoRepository<Projections.CustomerDetail> customerRepository)
    : INotificationHandler<CustomerCreatedDomainEvent>
{

    public async Task Handle(CustomerCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await customerRepository.OnReplaceAsync(
            new Projections.CustomerDetail(notification.FullName, notification.Email,
                notification.PhoneNumber) { Id = notification.Id, Version = notification.Version }, e => e.Version < notification.Version,
            cancellationToken);
    }
}