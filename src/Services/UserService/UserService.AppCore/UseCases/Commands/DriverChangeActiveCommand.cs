using AutoMapper;
using Core.Domain;
using Core.EventStore;
using MediatR;
using UserService.AppCore.Domain;
using UserService.AppCore.UseCases.Dtos;

namespace UserService.AppCore.UseCases.Commands;

public record DriverChangeActiveCommand(Guid Id) : ICommand<DriverDto>
{
    internal class Handler(IEventStoreService eventStore, IMapper mapper)
        : IRequestHandler<DriverChangeActiveCommand, ResultModel<DriverDto>>
    {

        public async Task<ResultModel<DriverDto>> Handle(DriverChangeActiveCommand request, CancellationToken cancellationToken)
        {
            var driver = await eventStore.LoadEventsAsync<Driver>(request.Id, cancellationToken);
            driver.ChangeActive();
            await eventStore.ApplyDomainEvents(driver);
            return ResultModel<DriverDto>.Create(mapper.Map<DriverDto>(driver));
        }
    }
}