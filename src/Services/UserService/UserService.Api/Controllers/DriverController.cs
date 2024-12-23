using Infrastructure.Controllers;
using Microsoft.AspNetCore.Mvc;
using UserService.AppCore.UseCases.Commands;

namespace UserService.Api.Controllers;

public class DriverController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> HandleCreateDriverAsync(CreatedDriverCommand command, CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }

    [HttpPut("/{id:guid}/active")]
    public async Task<IActionResult> HandleActiveDriverAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(new DriverChangeActiveCommand(id), cancellationToken));
    }
    [HttpPut("/{id:guid}/inactive")]
    public async Task<IActionResult> HandleInActiveDriverAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(new DriverChangeInActiveCommand(id), cancellationToken));
    }

}