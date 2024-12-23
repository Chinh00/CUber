using AutoMapper;
using TripService.AppCore.Domain;
using TripService.AppCore.UseCases.Dtos;

namespace TripService.Infrastructure.Configs;

public sealed class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<Booking, BookingDto>();
    }
}