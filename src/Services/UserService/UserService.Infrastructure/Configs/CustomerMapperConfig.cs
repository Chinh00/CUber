using AutoMapper;
using UserService.AppCore.Domain;
using UserService.AppCore.UseCases.Dtos;

namespace UserService.Infrastructure.Configs;

public class CustomerMapperConfig : Profile
{
    public CustomerMapperConfig()
    {
        CreateMap<Customer, CustomerDto>();
    }
}