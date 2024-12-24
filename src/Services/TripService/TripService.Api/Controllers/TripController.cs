using Infrastructure.Controllers;
using Microsoft.AspNetCore.Mvc;
using TripService.AppCore.UseCases.Commands;

namespace TripService.Api.Controllers;

public class TripController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> HandleCreateTripAsync(CreateTripCommand command, CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }
}