using Infrastructure.Mongodb;

namespace DriverService.AppCore.UseCases;

public static class Projections
{
    public record DriverDetail(string FullName, string Email, string PhoneNumber, VehicleDetail VehicleDetail = default) : Projection;
    public record VehicleDetail(Guid Id, string VehicleName, string NumberId, string VehicleType); 
}