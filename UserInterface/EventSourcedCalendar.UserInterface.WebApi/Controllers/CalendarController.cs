using EventSourcedCalendar.Core.Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcedCalendar.UserInterface.WebApi.Controllers;

public record CreateCalendarRequest(string Name);
public record RenameCalendarRequest(string Name);

[ApiController]
[Route("calendars")]
public class CalendarController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateCalendar(
        [FromBody] CreateCalendarRequest request,
        [FromServices] ICreateCalendarCommandHandler handler,
        CancellationToken cancellationToken)
    {
        await handler.CreateCalendar(request.Name, cancellationToken);
        return Ok();
    }

    [HttpPatch("{calendarId}")]
    public async Task<IActionResult> RenameCalendar(
        [FromBody] RenameCalendarRequest request,
        [FromRoute] Guid calendarId,
        [FromQuery] ulong version,
        [FromServices] IRenameCalendarCommandHandler handler,
        CancellationToken cancellationToken)
    {
        await handler.RenameCalendar(calendarId, request.Name, version, cancellationToken);
        return Ok();
    }
}
