namespace EventSourcedCalendar.Core.Application.Commands;

public interface ICreateCalendarCommandHandler
{
    Task CreateCalendar(string name, CancellationToken cancellationToken = default);
}