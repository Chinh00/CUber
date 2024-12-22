using MassTransit;

namespace TripService.Infrastructure.Masstransit.StateMachine;

public class BookingDefinition : SagaDefinition<BookingState>
{
    public BookingDefinition()
    {
        
    }
}