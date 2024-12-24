using MassTransit;
using Services;

namespace TripService.Infrastructure.Masstransit.StateMachine;

public class BookingState : ISagaVersion, SagaStateMachineInstance
{ 
    public Guid TripId { get; set; }
    public Guid CorrelationId { get; set; }
    public int Version { get; set; }
    public string CurrentState { get; set; } = default!;
    public DateTime? BookingDate { get; set; }
    
    public List<LocationDetail> Locations { get; set; } = default!;
}