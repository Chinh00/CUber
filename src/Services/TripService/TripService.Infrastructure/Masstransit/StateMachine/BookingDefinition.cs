using MassTransit;

namespace TripService.Infrastructure.Masstransit.StateMachine;

public class BookingDefinition : SagaDefinition<BookingState>
{
    public BookingDefinition()
    {
        ConcurrentMessageLimit = 10;
    }
    protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, ISagaConfigurator<BookingState> sagaConfigurator,
        IRegistrationContext context)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 5000, 10000));
        endpointConfigurator.UseInMemoryOutbox();
    }
}