using EventSourcedCalendar.Core.Domain;

namespace EventSourcedCalendar.Core.Application.Commands;

public class RenameCalendarCommandHandler : IRenameCalendarCommandHandler
{
    private readonly IRepository<Calendar> _calendarRepository;

    public RenameCalendarCommandHandler(
        IRepository<Calendar> calendarRepository)
    {
        _calendarRepository = calendarRepository;
    }

    public async Task RenameCalendar(
        Guid calendarId,
        string name,
        ulong originalVersion,
        CancellationToken cancellationToken)
    {
        var calendar = await _calendarRepository.GetById(calendarId, cancellationToken);
        calendar.Rename(name);
        await _calendarRepository.Save(calendar, originalVersion, cancellationToken);
    }
}
