using Core.Domain;
using MediatR;
using TripService.AppCore.UseCases.Dtos;

namespace TripService.AppCore.UseCases.Commands;

public record CreateTripCommand : ICommand<BookingDto>
{
    internal class Handler : IRequestHandler<CreateTripCommand, ResultModel<BookingDto>>
    {
        public Task<ResultModel<BookingDto>> Handle(CreateTripCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}