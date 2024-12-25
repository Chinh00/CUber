using Contracts.Services;
using MassTransit;

namespace TripService.Infrastructure.Masstransit.StateMachine;

public class BookingStateMachine : MassTransitStateMachine<BookingState>
{
    private readonly ILogger<BookingStateMachine> _logger;
    public BookingStateMachine(ILogger<BookingStateMachine> logger)
    {
        _logger = logger;
        Event(() => TripCreated, configurator => configurator.CorrelateById(context => context.Message.TripId));
        Event(() => DriverInvite, configurator => configurator.CorrelateById(context => context.Message.TripId));
        Event(() => DriverNotfound, configurator => configurator.CorrelateById(context => context.Message.TripId));
        Event(() => TripPicked, configurator => configurator.CorrelateById(context => context.Message.TripId));
        Event(() => DriverCancel, configurator => configurator.CorrelateById(context => context.Message.TripId));
        Event(() => DriverReady, configurator => configurator.CorrelateById(context => context.Message.TripId));
        Event(() => TripEnd, configurator => configurator.CorrelateById(context => context.Message.TripId));
        Event(() => CustomerCancel, configurator => configurator.CorrelateById(context => context.Message.TripId));
        Event(() => PaymentSuccess, configurator => configurator.CorrelateById(context => context.Message.TripId));
        Event(() => PaymentFail, configurator => configurator.CorrelateById(context => context.Message.TripId));

        
        InstanceState(c => c.CurrentState);
        Initially(When(TripCreated).ThenAsync(async (context) =>
        {
            context.Saga.BookingDate = context.Message.BookingDate;
            context.Saga.Locations = context.Message.LocationDetails;
            context.Saga.TripId = context.Message.TripId;
            await SendAuditLog($"Trip created: {context.Message.TripId}");
        }).Produce(context => context.Init<MakeInvitedIntegrationEvent>(new
        {
            context.Saga.TripId,
            Locations = context.Message.LocationDetails
        })).TransitionTo(Started));
        During(Started,
            When(DriverInvite).ThenAsync(async c =>
            {
                await SendAuditLog($"Send invite to trip {c.Message.TripId}");
            }).TransitionTo(Invited),
            When(DriverNotfound).ThenAsync(async c =>
            {
                await SendAuditLog($"Notfound driver for trip {c.Message.TripId}");
            }).TransitionTo(Completed));
        During(Invited, When(TripPicked).ThenAsync(async c =>
        {
            await SendAuditLog($"Driver picked for trip {c.Message.TripId}");
        }).TransitionTo(Begin));
        
        During(Begin, Ignore(DriverInvite), 
            When(CustomerCancel).ThenAsync(async c =>
            {
                await SendAuditLog($"Customer cancel trip {c.Message.TripId}");
            }).TransitionTo(Cancel),
            When(DriverCancel).ThenAsync(async c =>
            {
                await SendAuditLog($"Driver cancel for trip {c.Message.TripId}");
            }).TransitionTo(Completed),
            When(DriverReady).ThenAsync(async c =>
            {
                await SendAuditLog($"Driver ready start trip {c.Message.TripId}");
            }).TransitionTo(End));
        During(End, When(TripEnd).ThenAsync(async c =>
        {
            await SendAuditLog($"Trip end {c.Message.TripId}");
        }).TransitionTo(PaymentProcess));
        
        During(PaymentProcess, When(PaymentSuccess).ThenAsync(async c =>
        {
            await SendAuditLog($"Payment success for trip {c.Saga.TripId}");
        }).TransitionTo(Completed),
            When(PaymentFail).ThenAsync(async c =>
            {
                await SendAuditLog($"Payment failed for trip {c.Message.TripId}");
            }).TransitionTo(Cancel));
    }
    
    
    public State Started { get; private set; }
    public State Invited { get; private set; }
    public State Begin { get; private set; }
    public State End { get; private set; }
    
    public State PaymentProcess { get; private set; }
    
    public State Completed { get; private set; }
    public State Cancel { get; private set; }

    
    public Event<TripCreatedIntegrationEvent> TripCreated { get; private set; }
    public Event<DriverInviteIntegrationEvent> DriverInvite { get; private set; }
    public Event<DriverNotfoundIntegrationEvent> DriverNotfound { get; private set; }
    public Event<TripPickedIntegrationEvent> TripPicked { get; private set; }

    public Event<DriverCancelTripIntegrationEvent> DriverCancel { get; private set; }
    public Event<DriverReadyIntegrationEvent> DriverReady { get; private set; }
    public Event<TripEndIntegrationEvent> TripEnd { get; private set; }
    public Event<CustomerCancelTripIntegrationEvent> CustomerCancel { get; private set; }
    public Event<PaymentSuccessIntegrationEvent> PaymentSuccess { get; private set; }
    public Event<PaymentFailIntegrationEvent> PaymentFail { get; private set; }


    async Task SendAuditLog(string message)
    {
        _logger.LogInformation(message);
        await Task.CompletedTask;
    }
}