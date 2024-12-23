using Infrastructure.SignaIR;
using MediatR;
using TrackingService.AppCore.UseCases.Commands;

namespace TrackingService.Api.Hubs;

public abstract class TrackingHub : HubBase
{
    private readonly ISender _sender;
    private readonly ILogger<TrackingHub> _logger;
    protected TrackingHub(ISender sender, ILogger<TrackingHub> logger)
    {
        _sender = sender;
        _logger = logger;
        HubName = "TrackingHub";
    }

    public async Task HandleUpdateLocationAsync(UpdateLocationCommand command)
    {
        await _sender.Send(command);
        _logger.LogInformation("Received update location");
    }


}