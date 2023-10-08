namespace EventSourcedCalendar.Infrastructure.EventStore;

public class EventStoreOptions
{
    public const string EventStore = "EventStore";

    public string ConnectionString { get; set; } = string.Empty;
}
