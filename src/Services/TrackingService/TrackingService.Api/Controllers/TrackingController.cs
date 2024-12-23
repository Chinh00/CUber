using Infrastructure.Controllers;
using Microsoft.AspNetCore.Mvc;
using TrackingService.AppCore.UseCases.Commands;

namespace TrackingService.Api.Controllers;

public class TrackingController : BaseController
{
    [HttpPost("shift")]
    public async Task<IActionResult> HandleCreateSessionShiftAsync(CreateSessionShiftCommand command, CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> HandleUpdateLocationAsync(UpdateLocationCommand command,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }
}