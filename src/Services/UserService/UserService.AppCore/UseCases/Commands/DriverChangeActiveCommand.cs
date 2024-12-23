using AutoMapper;
using Core.Domain;
using Core.EventStore;
using Core.Repository;
using MediatR;
using UserService.AppCore.Domain;
using UserService.AppCore.UseCases.Dtos;

namespace UserService.AppCore.UseCases.Commands;

public record DriverChangeActiveCommand(Guid Id) : ICommand<DriverDto>
{
    internal class Handler(IMapper mapper, IRepository<Driver> repository)
        : IRequestHandler<DriverChangeActiveCommand, ResultModel<DriverDto>>
    {
        public async Task<ResultModel<DriverDto>> Handle(DriverChangeActiveCommand request, CancellationToken cancellationToken)
        {
            
            return ResultModel<DriverDto>.Create(mapper.Map<DriverDto>(new Driver()));
        }
    }
}