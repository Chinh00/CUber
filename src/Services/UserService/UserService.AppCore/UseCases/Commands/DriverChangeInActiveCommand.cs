using AutoMapper;
using Core.Domain;
using Core.EventStore;
using MediatR;
using UserService.AppCore.Domain;
using UserService.AppCore.UseCases.Dtos;

namespace UserService.AppCore.UseCases.Commands;

public record DriverChangeInActiveCommand(Guid Id) : ICommand<DriverDto>
{
    internal class Handler(IMapper mapper)
        : IRequestHandler<DriverChangeInActiveCommand, ResultModel<DriverDto>>
    {

        public async Task<ResultModel<DriverDto>> Handle(DriverChangeInActiveCommand request, CancellationToken cancellationToken)
        {
            
            return ResultModel<DriverDto>.Create(mapper.Map<DriverDto>(new Driver()));
        }
    }
}