using AutoMapper;
using Contracts.Services;
using Core.Domain;
using Core.Repository;
using MassTransit;
using MediatR;
using Services;
using TripService.AppCore.Domain;
using TripService.AppCore.UseCases.Dtos;

namespace TripService.AppCore.UseCases.Commands;

public record CreateTripCommand(
    Guid CustomerId,
    DateTime BookingDate,
    List<LocationDetail> LocationDetails) : ICommand<BookingDto>
{

    internal class Handler(
        IRepository<Booking> repository,
        IMapper mapper,
        ITopicProducer<TripCreatedIntegrationEvent> topicProducer)
        : IRequestHandler<CreateTripCommand, ResultModel<BookingDto>>
    {
        public async Task<ResultModel<BookingDto>> Handle(CreateTripCommand request,
            CancellationToken cancellationToken)
        {
            var booking = new Booking()
            {
                CustomerId = request.CustomerId,
                BookingDate = request.BookingDate,
            };
            request?.LocationDetails.Select(e => new Location()
            {
                BookingId = booking.Id,
                LocationName = e.LocationName,
                Lat = e.Latitude,
                Lng = e.Longitude
            }).Aggregate(booking.Locations, (x, y) => { x.Add(y); return x; } );
            await repository.AddAsync(booking, cancellationToken);
            await topicProducer.Produce(new
            {
                TripId = booking.Id,
                CustomerId = request.CustomerId,
                BookingDate = booking.BookingDate,
                LocationDetails = request.LocationDetails
            }, cancellationToken);
            return ResultModel<BookingDto>.Create(mapper.Map<BookingDto>(booking));
        }
    }
}