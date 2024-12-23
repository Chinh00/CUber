using Infrastructure.Mongodb;

namespace DriverService.AppCore.UseCases;

public static class Projections
{
    public record DriverDetail() : Projection;
}