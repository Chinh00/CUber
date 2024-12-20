using MassTransit;

namespace TripService.Infrastructure.Masstransit.StateMachine;

public class BookingState : ISagaVersion
{
    public Guid CorrelationId { get; set; }
    public int Version { get; set; }
}