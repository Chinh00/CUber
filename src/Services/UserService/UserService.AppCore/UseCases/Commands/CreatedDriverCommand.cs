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

public record CreatedDriverCommand(string FullName, string Email, string PhoneNumber) : ICommand<DriverDto>
{
    internal class Handler(
        IEventStoreService eventStoreService,
        IEventBusService eventBusService,
        ISchemaRegistryClient schemaRegistryClient,
        IRepository<DriverOutbox> repository,
        IMapper mapper) :
        OutboxHandler<DriverOutbox>(schemaRegistryClient, repository),
        IRequestHandler<CreatedDriverCommand, ResultModel<DriverDto>>
    {
        public async Task<ResultModel<DriverDto>> Handle(CreatedDriverCommand request, CancellationToken cancellationToken)
        {
            var (fullName, email, phoneNumber) = request;
            Driver driver = new();
            driver.Create(fullName, email, phoneNumber);
            await eventStoreService.ApplyDomainEvents(driver);
            driver.DomainEvents.ToList().ForEach( async e => await eventBusService.PublishEventAsync((dynamic)e, cancellationToken));
            var driverCreatedIntegrationEvent = new DriverCreatedIntegrationEvent()
                { FullName = fullName, Id = driver.Id.ToString(), PhoneNumber = driver.PhoneNumber };
            await SendToOutboxAsync(driver, () => (new DriverOutbox(), driverCreatedIntegrationEvent, "driver_cdc_events"), cancellationToken);
            return ResultModel<DriverDto>.Create(mapper.Map<DriverDto>(driver));
        }
    }
}