using Infrastructure.Controllers;
using Microsoft.AspNetCore.Mvc;
using UserService.AppCore.UseCases.Commands;

namespace UserService.Api.Controllers;

public class UserController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> HandleCreateUserAsync(CreateUserCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }
}