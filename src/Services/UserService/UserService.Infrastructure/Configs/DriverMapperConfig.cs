using AutoMapper;
using UserService.AppCore.Domain;
using UserService.AppCore.UseCases.Dtos;

namespace UserService.Infrastructure.Configs;

public class DriverMapperConfig : Profile
{
    public DriverMapperConfig()
    {
        CreateMap<Driver, DriverDto>();
    }
}