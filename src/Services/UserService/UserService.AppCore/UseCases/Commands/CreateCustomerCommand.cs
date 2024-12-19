using AutoMapper;
using Core.Domain;
using Core.EventStore;
using MediatR;
using UserService.AppCore.Domain;
using UserService.AppCore.UseCases.Dtos;

namespace UserService.AppCore.UseCases.Commands;

public record CreateCustomerCommand(string FullName, string Email, string PhoneNumber) : ICommand<CustomerDto>
{
    internal class Handler(IEventStoreService eventStore, IMapper mapper, IEventBusService eventBusService) : IRequestHandler<CreateCustomerCommand, ResultModel<CustomerDto>>
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
            
            return ResultModel<CustomerDto>.Create(mapper.Map<CustomerDto>(customer));
        }
    }
}