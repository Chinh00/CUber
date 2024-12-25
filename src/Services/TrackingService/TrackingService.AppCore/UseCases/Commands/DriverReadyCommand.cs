using Contracts.Services;
using Core.Domain;
using Core.Repository;
using MassTransit;
using MediatR;
using TrackingService.AppCore.Domain;
using TrackingService.AppCore.UseCases.Dtos;

namespace TrackingService.AppCore.UseCases.Commands;

public record DriverReadyCommand(Guid VehicleId, Guid TripId) : ICommand<bool>
{
    internal class Handler(ITopicProducer<TripEndIntegrationEvent> integrationEventProducer)
        : IRequestHandler<DriverReadyCommand, ResultModel<bool>>
    {
        public async Task<ResultModel<bool>> Handle(DriverReadyCommand request, CancellationToken cancellationToken)
        {
            await integrationEventProducer.Produce(new { request.TripId }, cancellationToken);
            return ResultModel<bool>.Create(true);
        }
    }
}