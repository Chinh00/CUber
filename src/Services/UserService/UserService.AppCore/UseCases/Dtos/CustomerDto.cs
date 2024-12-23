using AutoMapper;
using UserService.AppCore.Domain;

namespace UserService.AppCore.UseCases.Dtos;

public record CustomerDto
{
    public Guid Id { get; set; }
    public string FullName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
}

public class CustomerMapperConfig : Profile
{
    public CustomerMapperConfig()
    {
        CreateMap<Customer, CustomerDto>();
    }
}
