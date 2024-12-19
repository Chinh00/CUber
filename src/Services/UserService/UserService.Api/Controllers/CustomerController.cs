using Infrastructure.Controllers;
using Microsoft.AspNetCore.Mvc;
using UserService.AppCore.UseCases.Commands;

namespace UserService.Api.Controllers;

public class CustomerController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> HandleCreateCustomerAsync(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }
}