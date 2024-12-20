using AutoMapper;
using Confluent.SchemaRegistry;
using Core.Domain;
using Core.EventStore;
using Core.Repository;
using Infrastructure.OutboxHandler;
using MediatR;
using Services;
using UserService.AppCore.Domain;
using UserService.AppCore.Domain.Outbox;
using UserService.AppCore.UseCases.Dtos;

namespace UserService.AppCore.UseCases.Commands;

public record CreateCustomerCommand(string FullName, string Email, string PhoneNumber) : ICommand<CustomerDto>
{
    internal class Handler(
        IEventStoreService eventStore,
        IMapper mapper,
        IEventBusService eventBusService,
        ISchemaRegistryClient schemaRegistryClient,
        IRepository<CustomerOutbox> repository)
        : OutboxHandler<CustomerOutbox>(schemaRegistryClient, repository),
            IRequestHandler<CreateCustomerCommand, ResultModel<CustomerDto>>
    {
        public async Task<ResultModel<CustomerDto>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            Customer customer = new();
            var (fullName, email, phoneNumber) = request;
            customer.Create(fullName, email, phoneNumber);
            await eventStore.ApplyDomainEvents(customer);
            
            foreach (var customerDomainEvent in customer.DomainEvents)
            {
                await eventBusService.PublishEventAsync((dynamic)customerDomainEvent, cancellationToken);
            }

            var customerCreatedIntegrationEvent = new CustomerCreatedIntegrationEvent()
            {
                FullName = fullName,
                Id = customer.Id.ToString(),
                PhoneNumber = phoneNumber
            };
            await SendToOutboxAsync(customer,
                () => (new CustomerOutbox(), customerCreatedIntegrationEvent, "customer_cdc_event"), cancellationToken);
            return ResultModel<CustomerDto>.Create(mapper.Map<CustomerDto>(customer));
        }
    }
}