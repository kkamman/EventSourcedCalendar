namespace EventSourcedCalendar.Core.Application.Commands;

public interface IRenameCalendarCommandHandler
{
    Task RenameCalendar(
        Guid calendarId,
        string name,
        ulong originalVersion,
        CancellationToken cancellationToken = default);
}