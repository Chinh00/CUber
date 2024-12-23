using AutoMapper;
using DriverService.AppCore.Domain;
using DriverService.AppCore.UseCases.Dtos;

namespace DriverService.Infrastructure.Configs;

public class DriverMapperConfig : Profile
{
    public DriverMapperConfig()
    {
        CreateMap<DriverInfo, DriverInfoDto>();
    }
}