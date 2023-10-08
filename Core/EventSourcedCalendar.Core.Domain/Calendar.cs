namespace EventSourcedCalendar.Core.Domain;

public record CalendarCreatedEvent(Guid Id, string Name) : IEvent;
public record CalendarRenamedEvent(Guid Id, string Name) : IEvent;

public class Calendar : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;

    private Calendar() { }

    public Calendar(Guid id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty.", nameof(name));
        }
        ApplyChange(new CalendarCreatedEvent(id, name));
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty.", nameof(name));
        }
        ApplyChange(new CalendarRenamedEvent(Id, name));
    }

    protected override void Apply(IEvent e)
    {
        if (e is CalendarCreatedEvent createdEvent)
        {
            Id = createdEvent.Id;
            Name = createdEvent.Name;
            return;
        }

        if (e is CalendarRenamedEvent renamedEvent)
        {
            Name = renamedEvent.Name;
            return;
        }

        throw new NotImplementedException();
    }
}
