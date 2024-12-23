using Contracts.Services;
using MassTransit;

namespace TripService.Infrastructure.Masstransit.StateMachine;

public class BookingStateMachine : MassTransitStateMachine<BookingState>
{
    public BookingStateMachine()
    {
        Event(() => TripCreated, configurator => configurator.CorrelateById(context => context.Message.TripId));
    }
    
    
    public State Started { get; private set; }
    public State Invited { get; private set; }
    public State Begin { get; private set; }
    public State End { get; private set; }
    public State Completed { get; private set; }
    
    public Event<TripCreatedIntegrationEvent> TripCreated { get; private set; }
    
}