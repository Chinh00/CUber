using AutoMapper;
using Core.Domain;
using Core.EventStore;
using MediatR;
using UserService.AppCore.Domain;
using UserService.AppCore.UseCases.Dtos;

namespace UserService.AppCore.UseCases.Commands;

public record UpdateCustomerCommand(Guid Id, string FullName, string Email, string PhoneNumber) : ICommand<CustomerDto>
{
    internal class Handler(IMapper mapper)
        : IRequestHandler<UpdateCustomerCommand, ResultModel<CustomerDto>>
    {
        public async Task<ResultModel<CustomerDto>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var (id, fullName, email, phoneNumber) = request;

            // var customer = await eventStore.LoadEventsAsync<Customer>(id, cancellationToken);
            // customer.Update(fullName, email, phoneNumber);
            // await eventStore.ApplyDomainEvents(customer);
            // customer.DomainEvents.ToList()
            //     .ForEach(async (e) => await eventBus.PublishEventAsync((dynamic)e, cancellationToken));
            return ResultModel<CustomerDto>.Create(mapper.Map<CustomerDto>(new Customer())); 
        }
    }
}