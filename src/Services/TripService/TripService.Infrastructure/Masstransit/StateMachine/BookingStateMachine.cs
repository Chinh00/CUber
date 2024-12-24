using Contracts.Services;
using MassTransit;

namespace TripService.Infrastructure.Masstransit.StateMachine;

public class BookingStateMachine : MassTransitStateMachine<BookingState>
{
    public BookingStateMachine()
    {
        Event(() => TripCreated, configurator => configurator.CorrelateById(context => context.Message.TripId));
        Event(() => MakeInvited, configurator => configurator.CorrelateById(context => context.Message.TripId));
        Event(() => DriverInvite, configurator => configurator.CorrelateById(context => context.Message.TripId));
        Event(() => DriverNotfound, configurator => configurator.CorrelateById(context => context.Message.TripId));
        Event(() => TripPicked, configurator => configurator.CorrelateById(context => context.Message.TripId));
        Event(() => DriverCancel, configurator => configurator.CorrelateById(context => context.Message.TripId));
        Event(() => DriverReady, configurator => configurator.CorrelateById(context => context.Message.TripId));

        
        InstanceState(c => c.CurrentState);
        Initially(When(TripCreated).ThenAsync(async (context) =>
        {
            context.Saga.BookingDate = context.Message.BookingDate;
            context.Saga.Locations = context.Message.LocationDetails;
            context.Saga.TripId = context.Message.TripId;
        }).Produce(context => context.Init<MakeInvitedIntegrationEvent>(new
        {
            context.Saga.TripId
        })).TransitionTo(Started));
        During(Started,
            When(DriverInvite).ThenAsync(async c =>
            {
                
            }).TransitionTo(Invited),
            When(DriverNotfound).ThenAsync(async c =>
            {
                
            }).TransitionTo(Completed));
        During(Invited, When(TripPicked).ThenAsync(async c =>
        {
            
        }).TransitionTo(Begin));
        During(Begin, Ignore(DriverInvite), 
            When(DriverCancel).ThenAsync(async c =>
            {
                
            }).TransitionTo(Completed),
            When(DriverReady).ThenAsync(async c =>
            {
                
            }).TransitionTo(End));
        
    }
    
    
    public State Started { get; private set; }
    public State Invited { get; private set; }
    public State Begin { get; private set; }
    public State End { get; private set; }
    public State Completed { get; private set; }
    
    public Event<TripCreatedIntegrationEvent> TripCreated { get; private set; }
    public Event<MakeInvitedIntegrationEvent> MakeInvited { get; private set; }
    public Event<DriverInviteIntegrationEvent> DriverInvite { get; private set; }
    public Event<DriverNotfoundIntegrationEvent> DriverNotfound { get; private set; }
    public Event<TripPickedIntegrationEvent> TripPicked { get; private set; }

    public Event<DriverCancelTripIntegrationEvent> DriverCancel { get; private set; }
    public Event<DriverReadyIntegrationEvent> DriverReady { get; private set; }

}