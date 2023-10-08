using EventSourcedCalendar.Core.Domain;

namespace EventSourcedCalendar.Core.Application.Commands;

public class CreateCalendarCommandHandler : ICreateCalendarCommandHandler
{
    private readonly IRepository<Calendar> _calendarRepository;

    public CreateCalendarCommandHandler(
        IRepository<Calendar> calendarRepository)
    {
        _calendarRepository = calendarRepository;
    }

    public async Task CreateCalendar(
        string name,
        CancellationToken cancellationToken)
    {
        var calendar = new Calendar(Guid.NewGuid(), name);
        await _calendarRepository.Save(calendar, expectedVersion: null, cancellationToken);
    }
}
