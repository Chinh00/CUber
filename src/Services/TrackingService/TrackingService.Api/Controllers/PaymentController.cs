using Infrastructure.Controllers;
using Microsoft.AspNetCore.Mvc;
using TrackingService.AppCore.UseCases.Commands;

namespace TrackingService.Api.Controllers;

public class PaymentController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> HandlePaymentTripAsync(PaymentCommand command,
        CancellationToken cancellationToken = default)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }
}