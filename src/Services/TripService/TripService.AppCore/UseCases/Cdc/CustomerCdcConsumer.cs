using Core.Repository;
using MediatR;
using Services;
using TripService.AppCore.Domain;

namespace TripService.AppCore.UseCases.Cdc;

public class CustomerCdcConsumer(IRepository<CustomerInfo> customerRepository)
    : INotificationHandler<CustomerCreatedIntegrationEvent>
{

    public async Task Handle(CustomerCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await customerRepository.AddAsync(new CustomerInfo()
        {
            Id = Guid.Parse(notification.Id),
            FullName = notification.FullName,
            PhoneNumber = notification.PhoneNumber,
        }, cancellationToken);
    }
}