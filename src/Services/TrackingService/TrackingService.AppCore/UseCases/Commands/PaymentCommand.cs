using Contracts.Services;
using Core.Domain;
using MassTransit;
using MediatR;

namespace TrackingService.AppCore.UseCases.Commands;
public record PaymentCommand(Guid TripId, Guid TransactionId) : ICommand<bool>

{
    internal class Handler(
        ITopicProducer<PaymentSuccessIntegrationEvent> topicProducer,
        ITopicProducer<PaymentFailIntegrationEvent> topicProducerFail)
        : IRequestHandler<PaymentCommand, ResultModel<bool>>
    {
        public async Task<ResultModel<bool>> Handle(PaymentCommand request, CancellationToken cancellationToken)
        {
            var (tripId, transactionId) = request;
            await topicProducer.Produce(new { TripId = tripId }, cancellationToken);
            
            return ResultModel<bool>.Create(true);
        }
    }
}