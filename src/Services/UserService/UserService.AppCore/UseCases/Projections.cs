using Infrastructure.Mongodb;

namespace UserService.AppCore.UseCases;

public static class Projections
{
    public record CustomerDetail(string FullName, string Email, string PhoneNumber) : Projection;
}