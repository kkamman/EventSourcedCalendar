using EventSourcedCalendar.Core.Domain;

namespace EventSourcedCalendar.Infrastructure.EventStore;

internal class StreamNameMap
{
    private readonly Dictionary<Type, string> _map = new()
    {
        { typeof(Calendar), "calendar" }
    };

    public string GetStreamNameForType<T>()
    {
        return _map[typeof(T)];
    }

    public string GetStreamNameForType(object obj)
    {
        return _map[obj.GetType()];
    }
}
