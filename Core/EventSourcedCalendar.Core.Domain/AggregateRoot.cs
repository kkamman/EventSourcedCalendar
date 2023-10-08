namespace EventSourcedCalendar.Core.Domain;

public abstract class AggregateRoot
{
    public Guid Id { get; protected set; }

    private readonly List<IEvent> _changes = new();

    public static T FromStoredEvents<T>(IEnumerable<IEvent> events) where T : AggregateRoot
    {
        var aggregate = (T)Activator.CreateInstance(typeof(T), nonPublic: true)!;

        foreach (var e in events)
        {
            aggregate.ApplyChange(e, isNewChange: false);
        }

        return aggregate;
    }

    public IReadOnlyList<IEvent> GetUncommittedChanges()
    {
        return _changes;
    }

    public void MarkChangesAsCommitted()
    {
        _changes.Clear();
    }

    protected void ApplyChange(IEvent e)
    {
        ApplyChange(e, isNewChange: true);
    }

    protected abstract void Apply(IEvent e);

    private void ApplyChange(IEvent e, bool isNewChange)
    {
        Apply(e);

        if (isNewChange)
        {
            _changes.Add(e);
        }
    }
}
