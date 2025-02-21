using DriverService.AppCore.UseCases.Commands;
using Infrastructure.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriverService.Api.Controllers;

public class DriverController : BaseController
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> HandleAddVehicleAsync(DriverAddVehicleCommand command, CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }
}