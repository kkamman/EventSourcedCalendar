using EventSourcedCalendar.Core.Domain;

namespace EventSourcedCalendar.Infrastructure.EventStore;

internal class EventTypeNameMap : TypeMap
{
    public EventTypeNameMap() : base(new()
    {
        {  typeof(CalendarCreatedEvent), "calendarCreated" },
        {  typeof(CalendarRenamedEvent), "calendarRenamed" }
    })
    {
    }
}
